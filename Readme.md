<div align="center">

# 🤖 Arbitrage Bot V1 — Flash Loan Controller

<img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white"/>
<img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"/>
<img src="https://img.shields.io/badge/Nethereum-3C3C3D?style=for-the-badge&logo=ethereum&logoColor=white"/>
<img src="https://img.shields.io/badge/Anvil-FFCB47?style=for-the-badge&logo=ethereum&logoColor=black"/>

**First version of the off-chain Flash Loan controller — local development environment**

*Authenticates, binds a contract and fires `solicitarPrestamo` — the first real trigger.*

**🌍 [English](#-english-version) · 🇪🇸 [Español](#-versión-en-español)**

</div>

---

## 🇪🇸 Versión en Español

### 🎮 La Analogía del Mando a Distancia

> **El Mando *(este código C#)*** tiene configuradas de fábrica las frecuencias exactas *(RPC URL y clave privada)*. Su único trabajo es preparar el paquete de datos con las instrucciones precisas — cuánto dinero pedir y a qué pool ir — y emitir la señal pulsando el botón de `solicitarPrestamo`.
>
> **El Receptor *(el Smart Contract local)*** es la televisión que recibe la señal. Al captar la instrucción, ejecuta la lógica interna del préstamo en la red de pruebas.

---

### ⚙️ Flujo de Ejecución
```
Program.cs
    │
    ├── 1. Conexión RPC Local
    │       └── Web3 client → http://127.0.0.1:8545 (Anvil / Hardhat)
    │
    ├── 2. Autenticación de Cuenta
    │       └── new Account(privateKey) → firma de transacciones habilitada
    │
    ├── 3. Instanciación del Contrato
    │       └── ABI mínima → mapea función solicitarPrestamo(cantidad, poolAddress)
    │
    ├── 4. Vinculación
    │       └── contractAddress → contrato desplegado en nodo local
    │
    └── 5. Disparo
            └── solicitarPrestamo(cantidad, poolAddress) → TX enviada
```

---

### 🔬 V1 vs Versiones Posteriores

| Característica | V1 *(este repo)* | Versiones avanzadas |
|:---|:---|:---|
| Configuración | Hardcodeada en código | Variables `.env` |
| Red | Local (Anvil) | BSC / Ethereum Mainnet |
| Monitorización | Manual | Bucle autónomo continuo |
| Detección de spread | No | Sí (RealPriceBrain) |
| Gestión de riesgo | No | TP / SL automático |

> Esta versión es el **punto de partida** — demuestra que la conexión, autenticación y disparo funcionan antes de añadir complejidad.

---

### 🛠️ Tech Stack

| Capa | Tecnología |
|:---|:---|
| Lenguaje | C# / .NET 10.0 |
| Web3 Integration | Nethereum |
| Nodo local | Anvil / Hardhat |
| Red objetivo | EVM local (http://127.0.0.1:8545) |
| Configuración | Hardcodeada (V1) |

---

### 🏗️ Estructura del Proyecto
```
05_ArbitrageBot/
├── Program.cs                  # Lógica principal del bot V1
├── 05_ArbitrageBot.csproj      # Proyecto .NET
├── 05_ArbitrageBot.sln         # Solución
└── README.md
```

---

### 🚀 Configuración y Ejecución

**1. Levantar nodo local con Anvil**
```bash
anvil
# Puerto disponible: http://127.0.0.1:8545
```

**2. Verificar variables en `Program.cs`**
```csharp
string contractAddress = "0x_TU_CONTRATO_DESPLEGADO";
string poolAddress     = "0x_TU_POOL_LOCAL";
string privateKey      = "0x_CLAVE_PRIVADA_ANVIL";
```

**3. Ejecutar el bot**
```bash
dotnet run
```

---

### 🔗 Posición en el Ecosistema DeFi

El Arbitrage Bot V1 es el **primer disparo real** del ecosistema — fase 5:

| Fase | Repo | Rol |
|:---:|:---|:---|
| 1 | `Flash_Loans` | ⚡ Contrato Solidity — lógica on-chain |
| 2 | `09_ProfitBrain` | 🧠 Controlador off-chain — conecta con Mainnet |
| 3 | `03_FlashLoanDriver` | 🚀 Driver local — pruebas en entorno aislado |
| 4 | `04_MarketScanner` | 📡 Radar — lee precios en tiempo real sin gas |
| **5** | **`05_ArbitrageBot`** *(este)* | **🤖 V1 — primer disparo real de Flash Loan** |
| 6 | `10_RealPriceBrain` | 🎯 Cerebro — detecta spreads y dispara arbitraje |
| 7 | `13_SniperBot` | 🏹 Sniper — captura tokens nuevos en BSC |

---

### ⚖️ Disclaimer

Este proyecto es **exclusivamente para fines educativos e investigación DeFi**. Diseñado para entornos locales de prueba — no conecta con Mainnet por defecto.

---

### 🧑‍💻 Autor

**Héctor Oviedo** — Backend Developer & DeFi Researcher

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/hectorob/)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/HEO-80)

---
---

## 🇬🇧 English Version

### 🎮 The Remote Control Analogy

> **The Remote *(this C# code)*** has the exact frequencies factory-set *(RPC URL and private key)*. Its only job is to prepare the data packet with precise instructions — how much to borrow and which pool to use — and emit the signal by pressing the `solicitarPrestamo` button.
>
> **The Receiver *(the local Smart Contract)*** is the television that receives the signal. Upon catching the instruction, it executes the internal loan logic on the test network.

---

### ⚙️ Execution Flow
```
Program.cs
    │
    ├── 1. Local RPC Connection
    │       └── Web3 client → http://127.0.0.1:8545 (Anvil / Hardhat)
    │
    ├── 2. Account Authentication
    │       └── new Account(privateKey) → transaction signing enabled
    │
    ├── 3. Contract Instantiation
    │       └── minimal ABI → maps solicitarPrestamo(amount, poolAddress)
    │
    ├── 4. Binding
    │       └── contractAddress → deployed contract on local node
    │
    └── 5. Fire
            └── solicitarPrestamo(amount, poolAddress) → TX sent
```

---

### 🔬 V1 vs Later Versions

| Feature | V1 *(this repo)* | Advanced versions |
|:---|:---|:---|
| Configuration | Hardcoded in source | `.env` variables |
| Network | Local (Anvil) | BSC / Ethereum Mainnet |
| Monitoring | Manual | Continuous autonomous loop |
| Spread detection | No | Yes (RealPriceBrain) |
| Risk management | No | Automatic TP / SL |

> This version is the **starting point** — it proves that connection, authentication and firing work before adding complexity.

---

### 🚀 Setup & Execution

**1. Start local node with Anvil**
```bash
anvil
# Available at: http://127.0.0.1:8545
```

**2. Verify variables in `Program.cs`**
```csharp
string contractAddress = "0x_YOUR_DEPLOYED_CONTRACT";
string poolAddress     = "0x_YOUR_LOCAL_POOL";
string privateKey      = "0x_ANVIL_PRIVATE_KEY";
```

**3. Run the bot**
```bash
dotnet run
```

---

### 🔗 Position in the DeFi Ecosystem

Arbitrage Bot V1 is the **first real trigger** — phase 5 of the full ecosystem:

| Phase | Repo | Role |
|:---:|:---|:---|
| 1 | `Flash_Loans` | ⚡ Solidity contract — on-chain logic |
| 2 | `09_ProfitBrain` | 🧠 Off-chain controller — connects to Mainnet |
| 3 | `03_FlashLoanDriver` | 🚀 Local driver — isolated environment testing |
| 4 | `04_MarketScanner` | 📡 Radar — reads prices in real time, no gas |
| **5** | **`05_ArbitrageBot`** *(this)* | **🤖 V1 — first real Flash Loan trigger** |
| 6 | `10_RealPriceBrain` | 🎯 Brain — detects spreads and triggers arbitrage |
| 7 | `13_SniperBot` | 🏹 Sniper — captures new tokens on BSC |

---

### ⚖️ Disclaimer

This project is for **educational and DeFi research purposes only**. Designed for local test environments — does not connect to Mainnet by default.

---

### 🧑‍💻 Author

**Héctor Oviedo** — Backend Developer & DeFi Researcher

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/hectorob/)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/HEO-80)

---

<div align="center">
  <sub>Built with ☕ and DeFi research · <strong>Héctor Oviedo</strong> · Zaragoza, España</sub>
</div>
