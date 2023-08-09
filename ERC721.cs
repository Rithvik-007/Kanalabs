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
    static async Task Main(string[] args)
    {
        //  Ethereum account private key
        string privateKey = "35908726ddfbcf565259cb66551d12e020cdbd361a75dbc8c8d11b8b3a939e07";

        //  Ethereum node URL 
        string nodeUrl = "https://eth-sepolia.g.alchemy.com/v2/j2IXmLYH2wahQki0y9QWBM0K6hGkjrhu";

        // contract address 
        string contractAddress = "0x6624E5403a4f32366D6B7D700677f3908Ce12A07";

        var account = new Account(privateKey);
        var web3 = new Web3(account, nodeUrl);


        var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
        Console.WriteLine($"Balance: {Web3.Convert.FromWei(balance)} ETH");
    


    
    string recipientAddress = "0x6a649968F94BfDD1592AFD344e1e14fD15e183A7";

    
    string tokenURI = "https://gateway.pinata.cloud/ipfs/QmXQxQh4TJCvrTLAVukFMEmXuQAzdy3J748XSX9oQdtHGE/1.png";

    // Contract ABI
    string abi = @"
[
    {
        ""inputs"": [],
        ""stateMutability"": ""nonpayable"",
        ""type"": ""constructor""
    },
    {
        ""inputs"": [
            {
                ""internalType"": ""address"",
                ""name"": ""recipient"",
                ""type"": ""address""
            },
            {
                ""internalType"": ""string"",
                ""name"": ""tokenURI"",
                ""type"": ""string""
            }
        ],
        ""name"": ""mintNFT"",
        ""outputs"": [  
            {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
            }
        ],
        ""stateMutability"": ""nonpayable"",
        ""type"": ""function""
    }
]";

    var contract = web3.Eth.GetContract(abi, contractAddress);

    var mintNFTFunction = contract.GetFunction("mintNFT");
    var transactionInput = new TransactionInput()
    {
        From = account.Address,
        To = contractAddress,
        Gas = new HexBigInteger(300000)  
    };


    // Call the 'mintNFT' function to mint a new NFT 
    var transactionReceipt = await mintNFTFunction.SendTransactionAsync(transactionInput, recipientAddress, tokenURI); 
    Console.WriteLine($"Transaction Hash: {transactionReceipt}");
    }
}
