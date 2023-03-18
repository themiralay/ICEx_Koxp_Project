Option Explicit On
Imports System.Security.Cryptography
Public NotInheritable Class Crypto
    Private TripleDes As New TripleDESCryptoServiceProvider
    Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()
        Dim sha1 As New SHA1CryptoServiceProvider
        Dim keyBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)
        ReDim Preserve hash(length - 1)
        Return hash
    End Function
    Sub New(ByVal key As String)
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
    End Sub
    Public Function EncryptData(ByVal plaintext As String) As String
        Dim plaintextBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(plaintext)
        Dim ms As New System.IO.MemoryStream
        Dim encStream As New CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()
        Return Convert.ToBase64String(ms.ToArray)
    End Function
    Public Function DecryptData(
    ByVal encryptedtext As String) As String
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)
        Dim ms As New System.IO.MemoryStream
        Dim decStream As New CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function
End Class
