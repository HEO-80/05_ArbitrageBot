using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;

class Program
{
    // --- CONFIGURACIÓN DEL ROBOT ---
    static string rpcUrl = "http://127.0.0.1:8545";
    static string privateKey = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
    
    // OJO: ¡Asegúrate de que esta es la dirección de TU contrato FlashLoanBot desplegado!
    static string contractAddress = "0xD01F3b6e16828628746e0C6Be4258B81572ba549"; 
    
    // Pool de Uniswap DAI/WETH
    static string poolAddress = "0xC2e9F25Be6257c210d7Adf0D4Cd6E3E881ba25f8";

    // ABI mínima para llamar a 'solicitarPrestamo'
    static string botAbi = @"[{'inputs':[{'internalType':'uint256','name':'cantidad','type':'uint256'}],'name':'solicitarPrestamo','outputs':[],'stateMutability':'nonpayable','type':'function'}]";

    static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("   🤖 ARBITRAGE BOT V1 - VIGILANCIA Y EJECUCIÓN    ");
        Console.WriteLine("----------------------------------------------------");
        Console.ResetColor();

        // 1. Inicializar conexiones
        var account = new Account(privateKey);
        var web3 = new Web3(account, rpcUrl);
        var botContract = web3.Eth.GetContract(botAbi, contractAddress);
        var funcionSolicitar = botContract.GetFunction("solicitarPrestamo");

        // Objeto para leer Uniswap
        var slot0Function = new Slot0Function();
        slot0Function.FromAddress = account.Address;

        Console.WriteLine($"🔭 Vigilando Pool: {poolAddress.Substring(0, 10)}...");
        Console.WriteLine($"🔫 Gatillo listo en: {contractAddress.Substring(0, 10)}...");
        Console.WriteLine("----------------------------------------------------\n");

        while (true)
        {
            try
            {
                // --- PASO 1: ESCANEAR PRECIO (Ojos) ---
                var resultado = await web3.Eth.GetContractQueryHandler<Slot0Function>()
                    .QueryDeserializingToObjectAsync<Slot0OutputDTO>(slot0Function, poolAddress);

                double precioEth = CalcularPrecio(resultado.SqrtPriceX96);
                
                Console.Write($"[{DateTime.Now:HH:mm:ss}] Precio ETH: ${precioEth:F2} ");

                // --- PASO 2: TOMAR DECISIÓN (Cerebro) ---
                // SIMULACIÓN: Digamos que si el ETH vale más de 1000$, queremos ejecutar.
                // (En la vida real, aquí calcularías la diferencia entre Uniswap y SushiSwap)
                
                if (precioEth > 1000) 
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("=> ¡OPORTUNIDAD DETECTADA! DISPARANDO...");
                    Console.ResetColor();

                    // --- PASO 3: EJECUTAR FLASH LOAN (Manos) ---
                    BigInteger unMillon = BigInteger.Parse("1000000000000000000000000"); 

                    var receipt = await funcionSolicitar.SendTransactionAndWaitForReceiptAsync(
                        account.Address, 
                        new Nethereum.Hex.HexTypes.HexBigInteger(3000000), // Gas Limit 3M
                        new Nethereum.Hex.HexTypes.HexBigInteger(0),       // 0 ETH value
                        null, unMillon
                    );

                    // --- PASO 4: VERIFICAR RESULTADO ---
                    if (receipt.Status.Value == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("   ✅ ¡PROFIT! Operación confirmada.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("   🛡️ REVERT: Sin beneficio, operación cancelada.");
                    }
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("=> Esperando...");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"   Error ciclo: {ex.Message}"); // Ignoramos errores menores de red
                Console.ResetColor();
            }

            // Esperar 5 segundos antes del siguiente escaneo
            await Task.Delay(5000);
        }
    }

    // Función auxiliar para traducir matemáticas de Uniswap
    static double CalcularPrecio(BigInteger sqrtPriceX96)
    {
        double precioRaw = (double)sqrtPriceX96;
        double dosElevado96 = Math.Pow(2, 96);
        double precioFinal = Math.Pow(precioRaw / dosElevado96, 2);
        return 1 / precioFinal; // Inverso para DAI/ETH
    }
}

// --- CLASES DTO DE UNISWAP ---
[Function("slot0", typeof(Slot0OutputDTO))]
public class Slot0Function : FunctionMessage { }

[FunctionOutput]
public class Slot0OutputDTO : IFunctionOutputDTO
{
    [Parameter("uint160", "sqrtPriceX96", 1)]
    public BigInteger SqrtPriceX96 { get; set; }
    // (Omitimos el resto de parámetros que no usamos para limpiar código)
}