using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.Accounts;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using Nethereum.Signer;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.Standards.ERC20.ContractDefinition;
using Nethereum.Util;

class Program
{
    static async Task Main()
    {


        // Replace with the actual ABI of the ERC20 token contract
        string contractABI = @"[{
            ""inputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": ""cap"",
                ""type"": ""uint256""
                },
                {
                ""internalType"": ""uint256"",
                ""name"": ""reward"",
                ""type"": ""uint256""
                }
            ],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""constructor""
            },
            {
            ""anonymous"": false,
            ""inputs"": [
                {
                ""indexed"": true,
                ""internalType"": ""address"",
                ""name"": ""owner"",
                ""type"": ""address""
                },
                {
                ""indexed"": true,
                ""internalType"": ""address"",
                ""name"": ""spender"",
                ""type"": ""address""
                },
                {
                ""indexed"": false,
                ""internalType"": ""uint256"",
                ""name"": ""value"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""Approval"",
            ""type"": ""event""
            },
            {
            ""anonymous"": false,
            ""inputs"": [
                {
                ""indexed"": true,
                ""internalType"": ""address"",
                ""name"": ""from"",
                ""type"": ""address""
                },
                {
                ""indexed"": true,
                ""internalType"": ""address"",
                ""name"": ""to"",
                ""type"": ""address""
                },
                {
                ""indexed"": false,
                ""internalType"": ""uint256"",
                ""name"": ""value"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""Transfer"",
            ""type"": ""event""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""owner"",
                ""type"": ""address""
                },
                {
                ""internalType"": ""address"",
                ""name"": ""spender"",
                ""type"": ""address""
                }
            ],
            ""name"": ""allowance"",
            ""outputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""spender"",
                ""type"": ""address""
                },
                {
                ""internalType"": ""uint256"",
                ""name"": ""amount"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""approve"",
            ""outputs"": [
                {
                ""internalType"": ""bool"",
                ""name"": """",
                ""type"": ""bool""
                }
            ],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""account"",
                ""type"": ""address""
                }
            ],
            ""name"": ""balanceOf"",
            ""outputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": ""amount"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""burn"",
            ""outputs"": [],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""account"",
                ""type"": ""address""
                },
                {
                ""internalType"": ""uint256"",
                ""name"": ""amount"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""burnFrom"",
            ""outputs"": [],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            },
            {
            ""inputs"": [],
            ""name"": ""cap"",
            ""outputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""_adress"",
                ""type"": ""address""
                }
            ],
            ""name"": ""checkBalance"",
            ""outputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [],
            ""name"": ""decimals"",
            ""outputs"": [
                {
                ""internalType"": ""uint8"",
                ""name"": """",
                ""type"": ""uint8""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""spender"",
                ""type"": ""address""
                },
                {
                ""internalType"": ""uint256"",
                ""name"": ""subtractedValue"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""decreaseAllowance"",
            ""outputs"": [
                {
                ""internalType"": ""bool"",
                ""name"": """",
                ""type"": ""bool""
                }
            ],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            },
            {
            ""inputs"": [],
            ""name"": ""destroy"",
            ""outputs"": [],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""spender"",
                ""type"": ""address""
                },
                {
                ""internalType"": ""uint256"",
                ""name"": ""addedValue"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""increaseAllowance"",
            ""outputs"": [
                {
                ""internalType"": ""bool"",
                ""name"": """",
                ""type"": ""bool""
                }
            ],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            },
            {
            ""inputs"": [],
            ""name"": ""name"",
            ""outputs"": [
                {
                ""internalType"": ""string"",
                ""name"": """",
                ""type"": ""string""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [],
            ""name"": ""returninitialSupply"",
            ""outputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": ""reward"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""setBlockReward"",
            ""outputs"": [],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            },
            {
            ""inputs"": [],
            ""name"": ""symbol"",
            ""outputs"": [
                {
                ""internalType"": ""string"",
                ""name"": """",
                ""type"": ""string""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [],
            ""name"": ""totalSupply"",
            ""outputs"": [
                {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""to"",
                ""type"": ""address""
                },
                {
                ""internalType"": ""uint256"",
                ""name"": ""amount"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""transfer"",
            ""outputs"": [
                {
                ""internalType"": ""bool"",
                ""name"": """",
                ""type"": ""bool""
                }
            ],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            },
            {
            ""inputs"": [
                {
                ""internalType"": ""address"",
                ""name"": ""from"",
                ""type"": ""address""
                },
                {
                ""internalType"": ""address"",
                ""name"": ""to"",
                ""type"": ""address""
                },
                {
                ""internalType"": ""uint256"",
                ""name"": ""amount"",
                ""type"": ""uint256""
                }
            ],
            ""name"": ""transferFrom"",
            ""outputs"": [
                {
                ""internalType"": ""bool"",
                ""name"": """",
                ""type"": ""bool""
                }
            ],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
            }
        ]";
try
        {

        string contractAddress = "0x88ae0b9bfc9D68B45bF6be0377835d5ABdb2d35a";


        //string senderAddress = "0x426f5155d140a0BB115c2634534ac6b7806f1DEa";
        string privateKey = "35908726ddfbcf565259cb66551d12e020cdbd361a75dbc8c8d11b8b3a939e07";

       
        //string senderPrivateKey = "35908726ddfbcf565259cb66551d12e020cdbd361a75dbc8c8d11b8b3a939e07";\
        string recipientAddress = "0x25D7bF3ECD86C873a63A98A32fC935a9876548F1";
        decimal amountToSend = 0.001m;
        string addressToCheck = "0x426f5155d140a0BB115c2634534ac6b7806f1DEa";



        var web3 = new Web3(new Account(privateKey), "https://eth-sepolia.g.alchemy.com/v2/j2IXmLYH2wahQki0y9QWBM0K6hGkjrhu");


        var contract = web3.Eth.GetContract(contractABI, contractAddress);

        var totalSupplyfunction = contract.GetFunction("totalSupply");
        var transferFunction = contract.GetFunction("transfer");
        var balanceOfFunction = contract.GetFunction("balanceOf");
        
        BigInteger balance = await balanceOfFunction.CallAsync<BigInteger>(addressToCheck);
        BigInteger totalSupply = await totalSupplyfunction.CallAsync<BigInteger>();

            // Convert the balance to a readable format based on the token's decimals
            decimal decimals1 = 18; // Replace with the actual decimals of your token
            decimal readableBalance = Web3.Convert.FromWei(balance, (UnitConversion.EthUnit)decimals1);

            
            Console.WriteLine($"Token balance of address {addressToCheck}: {readableBalance}");
            decimal readableBalanceContract = Web3.Convert.FromWei(totalSupply, (UnitConversion.EthUnit)decimals1);
            Console.WriteLine($"Token supply of contract {contractAddress}: {readableBalanceContract}");





        
            // Convert the amount to the contract's smallest unit (e.g., wei) based on decimals
            decimal decimals = 18; // Replace with the actual decimals of your token
            BigInteger amount = Web3.Convert.ToWei(amountToSend, (Nethereum.Util.UnitConversion.EthUnit)decimals);

            var transactionInput = new TransactionInput()
            {
                From = web3.TransactionManager.Account.Address,
                To = recipientAddress,
                //Data = transferFunction.GetData(recipientAddress, amount),
                Gas = new HexBigInteger(300000)  
            };

            // Send the transfer transaction
            var transactionReceipt = await transferFunction.SendTransactionAsync(transactionInput);

            Console.WriteLine("Transaction sent. Transaction hash: " + transactionReceipt);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        


       




    }
}
