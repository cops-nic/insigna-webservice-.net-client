Imports System.ServiceModel

Module Module1

    Sub Main()
        System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
        'Creamos el proxy que servirá como cliente y le especificamos el endpoint al cual debe apuntar
        Dim clientProxy As InsignaWs.InsignaSOAPWSClient
        clientProxy = New InsignaWs.InsignaSOAPWSClient
        Dim wrapper As InsignaWs.cfdiInfoWrapper
        wrapper = New InsignaWs.cfdiInfoWrapper
        Dim result As InsignaWs.CfdiInfoResult
        result = New InsignaWs.CfdiInfoResult

        Try
            'Indicamos las credenciales con las que se idenficiará el cliente en el servicio web
            clientProxy.ClientCredentials.UserName.UserName = "usuario"
            clientProxy.ClientCredentials.UserName.Password = "password"
            'Se llama a los métodos del Servicio web mediante las clases que importamos
            wrapper.uuid = "753e9e17-39f2-44f1-91fe-c7c0bec617a0"
            result = clientProxy.getCfdiInfo(wrapper)
            Console.WriteLine("Response Code: {0} " + result.responseCode)
            Console.WriteLine("Response Description: {0} " + result.responseDescription)
            Console.WriteLine("Execution Time: {0} " + result.executionTime)
            Console.WriteLine("Request Date {0}" + result.requestDate)
            Console.WriteLine("Response Date: {0} " + result.responseDate)
            Console.WriteLine("Server transaction ID: {0} " + result.serverTransactionId)
            Console.WriteLine("Transaction ID: {0} " + result.transactionId)
            Console.WriteLine("CfdiStatus: {0} " + result.cfdiStatus)

            Console.ReadLine()
        Catch ex As FaultException(Of InsignaWs.operationFailed)
            ' Si ocurre algún error se imprime el código y la descripción
            Console.WriteLine("Código de error: " + ex.Detail.errorCode.ToString)
            Console.WriteLine("Descripción del error: " + ex.Detail.errorDescription)
            Console.ReadLine()
        End Try
    End Sub
    Public Function AcceptAllCertifications(ByVal sender As Object, ByVal certifications As System.Security.Cryptography.X509Certificates.X509Certificate,
                                            ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErros As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function
End Module