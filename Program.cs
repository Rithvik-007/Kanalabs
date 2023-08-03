using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string contractAddress = "0x011a61eAe262F7c7AdB52684D35F4BdC5583a443";
            string privateKey = "0xe7cd8b433aa647f431fe027229860da9b2df4679ae7df07fea2a3b5db5fe1210"; 

            await GetContractFunctionValue(contractAddress, privateKey);
            await GetContractBalance(contractAddress);

            Console.ReadLine();
        }

        static async Task GetContractFunctionValue(string contractAddress, string privateKey)
        {
            var web3 = new Web3("https://polygon-mumbai.g.alchemy.com/v2/12abLL_mMoov9iGV-VGRF9cEUnoIdfMq");

            
            var contractAbi = @"[
                {
                    ""inputs"": [],
                    ""name"": ""fund"",
                    ""outputs"": [],
                    ""stateMutability"": ""payable"",
                    ""type"": ""function""
                },
                {
                    ""inputs"": [],
                    ""stateMutability"": ""nonpayable"",
                    ""type"": ""constructor""
                },
                {
                    ""inputs"": [
                        {
                            ""internalType"": ""address"",
                            ""name"": ""funder"",
                            ""type"": ""address""
                        }
                    ],
                    ""name"": ""balanceofFunders"",
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
                    ""name"": ""contractBalance"",
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
                    ""name"": ""returnOwner"",
                    ""outputs"": [
                        {
                            ""internalType"": ""address"",
                            ""name"": """",
                            ""type"": ""address""
                        }
                    ],
                    ""stateMutability"": ""view"",
                    ""type"": ""function""
                }
            ]";

            var contract = web3.Eth.GetContract(contractAbi, contractAddress);

            
            var function = contract.GetFunction("returnOwner");
            var result = await function.CallAsync<string>();

            Console.WriteLine($"Contract function result (address): {result}");
        }

        static async Task GetContractBalance(string contractAddress)
        {
            var web3 = new Web3("https://polygon-mumbai.g.alchemy.com/v2/12abLL_mMoov9iGV-VGRF9cEUnoIdfMq");
            var balance = await web3.Eth.GetBalance.SendRequestAsync(contractAddress);

            var etherAmount = Web3.Convert.FromWei(balance.Value);
            Console.WriteLine($"Contract balance in Ether: {etherAmount}");
        }
    }
}
