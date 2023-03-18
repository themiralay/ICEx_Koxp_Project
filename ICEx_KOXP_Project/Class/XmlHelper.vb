Imports System.Xml
Imports System.IO
Imports System.Collections
Imports Json
Public Class XmlHelper
    Public Shared Sub GetXmlData()
        Dim path As String = AppDomain.CurrentDomain.BaseDirectory & "Settings\TBL Data\item_org_us.tbl.txt"
        Dim dataString As String() = File.ReadAllLines(path)
        Dim dataList As List(Of Object) = New List(Of Object)

        Dim dataDic As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
        For i As Integer = 0 To dataString.Length - 1
            Dim data As String() = dataString(i).Split(New Char() {"_"}, StringSplitOptions.RemoveEmptyEntries)
            Dim dataInnerDic As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            For j As Integer = 0 To data.Length - 1
                dataInnerDic.Add(j.ToString(), data(j))
            Next
            dataDic.Add(i.ToString(), dataInnerDic)
        Next
        Dim retString As String = Json.JsonParser.Serialize(dataDic)
        Console.WriteLine("1")
    End Sub
    Public Function CreateXmlNode(ByVal rootName As String, ByVal ParamArray nodes As Object()) As XmlDocument
        Dim xDoc As XmlDocument = New XmlDocument
        Dim xNode As XmlNode
        xNode = xDoc.CreateElement(rootName)
        xDoc.AppendChild(xNode)

        Dim firstNode As String = nodes(0).ToString()
        Dim xInnerNode = xDoc.CreateElement(firstNode)
        For i As Integer = 1 To nodes.Length - 1
            If (i Mod 2 = 0) Then
                xInnerNode = xDoc.CreateElement(nodes(i).ToString())
            Else
                If (nodes(i) Is New XmlDocument) Then
                    Dim newXDoc As XmlDocument = New XmlDocument
                    newXDoc.LoadXml(CType(nodes(i), XmlDocument).InnerXml)
                    For Each item As XmlNode In newXDoc.ChildNodes
                        Dim newNode As XmlNode = xDoc.ImportNode(item, True)
                        xInnerNode.AppendChild(newNode)
                        xNode.AppendChild(xInnerNode)
                    Next
                Else
                    xInnerNode.AppendChild(xDoc.CreateTextNode(nodes(i).ToString()))
                    xNode.AppendChild(xInnerNode)
                End If
            End If
        Next
        Return xDoc
    End Function
End Class
