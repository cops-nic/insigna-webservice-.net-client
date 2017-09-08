using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsignaWsClient.InsignaWs;
using System.ServiceModel;
using System.Net;
using System.ServiceModel.Channels;

namespace InsignaWsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creamos el proxy que servirá como cliente y le especificamos el endpoint al cual debe apuntar
            InsignaSOAPWSClient clientProxy = new InsignaSOAPWSClient();
            //Se configura el usuario y contraseña del usuario de Insigna
            clientProxy.ClientCredentials.UserName.UserName = "usuario";
            clientProxy.ClientCredentials.UserName.Password = "password";
            try
            {
                //Se llama a los métodos del Servicio web mediante las clases que importamos
                cfdiInfoWrapper wrapper = new cfdiInfoWrapper();
                wrapper.uuid = "905b5fec-720e-480c-b679-61d8a3d7c71a";
                CfdiInfoResult result = clientProxy.getCfdiInfo(wrapper);
                Console.WriteLine("Response Code: {0} " + result.responseCode);
                Console.WriteLine("Response Description: {0} " + result.responseDescription);
                Console.WriteLine("Request Date {0}" + result.requestDate);
                Console.WriteLine("Response Date: {0} " + result.responseDate);
                Console.WriteLine("Server transaction ID: {0} " + result.serverTransactionId);
                Console.WriteLine("Transaction ID: {0} " + result.transactionId);
                Console.WriteLine("Execution Time: {0} " + result.executionTime);
                Console.WriteLine("CfdiStatus: {0} " + result.cfdiStatus);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(result.cfdiStatus);
                Console.ReadLine();
            }
            catch (FaultException<operationFailed> e)
            {
                Console.WriteLine("Código de error: " + e.Detail.errorCode);
                Console.WriteLine("Descripción del error: " + e.Detail.errorDescription);

                Console.ReadLine();
            }
            catch (FaultException e)
            {
                Console.WriteLine("Error en la petición: " + e.Message);
                Console.ReadLine();
            }
        }
    }
}