# 🤖 Arbitrage Bot V1 - Vigilancia y Ejecución (C# Backend)

Este repositorio contiene la primera versión del controlador *off-chain* desarrollado en C# (.NET), diseñado para interactuar con un Smart Contract de préstamos rápidos (Flash Loans) en un entorno de desarrollo local.

## 🛠️ Especificaciones Técnicas

La aplicación es un cliente de consola construido en .NET que utiliza la librería `Nethereum` para gestionar la conexión Web3.

El flujo de ejecución (`Program.cs`) implementa las siguientes operaciones de red:
1.  **Conexión RPC Local:** Inicializa un cliente Web3 apuntando a un nodo de desarrollo local (`http://127.0.0.1:8545`, típicamente Anvil o Hardhat).
2.  **Autenticación de Cuenta:** Genera una instancia de `Account` utilizando una clave privada hardcodeada para la firma de transacciones en la red de pruebas.
3.  **Instanciación del Contrato:** Utiliza una ABI (Application Binary Interface) mínima inyectada en el código para mapear la función `solicitarPrestamo`.
4.  **Preparación de Ejecución:** Vincula la sesión autenticada con la dirección del contrato (`contractAddress`) y prepara la función objetivo para enviarle los parámetros requeridos (cantidad y dirección del pool).

---

## 🏎️ Arquitectura Conceptual

Para visualizar la interacción de esta primera versión con la blockchain local, utilizaremos la analogía de un mando a distancia:

* **El Mando a Distancia (Este código C#):** El programa tiene configuradas de fábrica las frecuencias exactas (RPC URL y Clave Privada). Su único trabajo en esta versión es preparar el paquete de datos con las instrucciones precisas (cuánto dinero pedir y a qué pool ir) y emitir la señal pulsando el botón de `solicitarPrestamo`.
* **El Receptor (El Smart Contract local):** Es la televisión que recibe la señal del mando. Al captar la instrucción, ejecuta la lógica interna del préstamo en la red de pruebas.

## 🚀 Configuración y Ejecución

Al ser la versión V1 orientada a desarrollo local, este bot **no utiliza** archivo `.env`. Las variables se configuran directamente en el código fuente.

1.  Asegúrate de tener tu nodo local (Anvil/Hardhat) ejecutándose en el puerto `8545`.
2.  Abre `Program.cs` y verifica que `contractAddress` y `poolAddress` coinciden con tus despliegues locales.
3.  Abre la terminal en la carpeta del proyecto y ejecuta el programa:
    ```bash
    dotnet run
    ```

---
---

# 🤖 Arbitrage Bot V1 - Vigilancia y Ejecución (C# Backend) [EN]

This repository contains the first version of the *off-chain* controller developed in C# (.NET), designed to interact with a Flash Loan Smart Contract in a local development environment.

## 🛠️ Technical Specifications

The application is a .NET console client utilizing the `Nethereum` library to manage the Web3 connection.

The execution flow (`Program.cs`) implements the following network operations:
1.  **Local RPC Connection:** Initializes a Web3 client pointing to a local development node (`http://127.0.0.1:8545`, typically Anvil or Hardhat).
2.  **Account Authentication:** Generates an `Account` instance using a hardcoded private key for transaction signing on the test network.
3.  **Contract Instantiation:** Uses a minimal ABI (Application Binary Interface) injected into the code to map the `solicitarPrestamo` (request loan) function.
4.  **Execution Preparation:** Links the authenticated session with the contract address (`contractAddress`) and prepares the target function to send the required parameters (amount and pool address).

---

## 🏎️ Conceptual Architecture

To visualize the interaction of this first version with the local blockchain, we use the analogy of a remote control:

* **The Remote Control (This C# code):** The program has the exact frequencies factory-set (RPC URL and Private Key). Its only job in this version is to prepare the data packet with precise instructions (how much money to request and which pool to go to) and emit the signal by pressing the `solicitarPrestamo` button.
* **The Receiver (The local Smart Contract):** It is the television that receives the signal from the remote. Upon catching the instruction, it executes the internal loan logic on the test network.

## 🚀 Setup & Execution

Being the V1 version oriented towards local development, this bot **does not use** a `.env` file. Variables are configured directly in the source code.

1.  Ensure your local node (Anvil/Hardhat) is running on port `8545`.
2.  Open `Program.cs` and verify that `contractAddress` and `poolAddress` match your local deployments.
3.  Open the terminal in the project folder and run the program:
    ```bash
    dotnet run
    ```