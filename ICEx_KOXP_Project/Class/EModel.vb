Imports System.Data.Entity.Core.EntityClient
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text

Module EModel
    Private ProjectUserId As Integer = 0
    Private ProjectUserEndDate As Date
    Private ProjectRestDate As TimeSpan
    Private ProjectDate As Date
    Private ProjectActive As Boolean = False
    Private ProjectMessage As String
    Private ProjectMessageActive As Boolean
    Private ProjectLoginActive As Boolean
    Private ProjectServerStatu As Boolean
    Private ProjectFreeTime As Boolean
    Private ProjectCrypto As New Crypto(GetGUID())
    Public Function GetGUID() As String
        Dim asm = Assembly.GetExecutingAssembly()
        Return asm.GetType().GUID.ToString()
    End Function
    Public Function GetConnectionString() As String
        Dim sqlBuilder As SqlConnectionStringBuilder = New SqlConnectionStringBuilder
        sqlBuilder.DataSource = ProjectCrypto.DecryptData(strProdDs)
        sqlBuilder.InitialCatalog = ProjectCrypto.DecryptData(strProdCat)
        sqlBuilder.PersistSecurityInfo = True
        sqlBuilder.MultipleActiveResultSets = True
        sqlBuilder.UserID = ProjectCrypto.DecryptData(strProdId)
        sqlBuilder.Password = ProjectCrypto.DecryptData(strProdPs)
        Dim entityBuilder As EntityConnectionStringBuilder = New EntityConnectionStringBuilder
        entityBuilder.Provider = ProjectCrypto.DecryptData(strProdPro)
        entityBuilder.ProviderConnectionString = sqlBuilder.ConnectionString
        entityBuilder.Metadata = ProjectCrypto.DecryptData("dmGoQ8g5PPpXHtkid4MykSq5cn8uX4jf")
        Return entityBuilder.ConnectionString
    End Function
    Public Function CSFreeTime() As Boolean
        Return ProjectFreeTime
    End Function
    Public Function CSServerActive() As Boolean
        Return ProjectServerStatu
    End Function
    Public Function CSRestDate() As TimeSpan
        Return ProjectRestDate
    End Function
    Public Function CSEndDate() As Date
        Return ProjectUserEndDate
    End Function
    Public Function CSActive() As Boolean
        Return ProjectActive
    End Function
    Public Function CSMessageActive() As Boolean
        Return ProjectMessageActive
    End Function
    Public Function CSMessage() As String
        Return ProjectMessage
    End Function
    Public Function GetMD5(source As String) As String
        Dim hash As MD5 = MD5.Create()
        Dim buffer = Encoding.Default.GetBytes(source)
        Return BitConverter.ToString(hash.ComputeHash(buffer)).Replace("-", "")
    End Function
    Public Function GetSHA256(source As String) As String
        Dim hash As SHA256 = SHA256.Create()
        Dim buffer = Encoding.Default.GetBytes(source)
        Return BitConverter.ToString(hash.ComputeHash(buffer)).Replace("-", "")
    End Function
    Public Function GetHashPass(ByVal password As String)
        Return GetSHA256(GetMD5(password))
    End Function
    Public Function GetSerialNumber() As String
        Dim DriveSerial As Long
        Dim fso As Object, Drv As Object
        fso = CreateObject("Scripting.FileSystemObject")
        Drv = fso.GetDrive(fso.GetDriveName(AppDomain.CurrentDomain.BaseDirectory))
        With Drv
            If .IsReady Then
                DriveSerial = .SerialNumber
            Else
                DriveSerial = -1
            End If
        End With
        Drv = Nothing
        fso = Nothing
        Return DriveSerial
    End Function
    Public Function CheckModel() As Boolean
        Try
            Using db As New ICExProject()
                ProjectServerStatu = db.Database.Exists()
                ServerStatus = ProjectServerStatu
                Return ProjectServerStatu
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub GetProjectSettings()
        Try
            Using db As New ICExProject()
                db.Database.Connection.Open()
                Dim result = (From i In db.cs_settings Where i.cs_setID = 1 Select i).SingleOrDefault()
                ProjectDate = result.cs_date
                ProjectActive = result.cs_active
                ProjectMessage = result.cs_message
                ProjectMessageActive = result.cs_messageactive
                ProjectLoginActive = result.cs_loginactive
                ProjectFreeTime = result.cs_freetime
                db.Database.Connection.Close()
            End Using
        Catch ex As Exception
            ProjectServerStatu = False
        End Try
    End Sub
    Public Function AddNewUser(ByVal userName As String, ByVal userPass As String) As Integer
        GetProjectSettings()
        If (CheckModel() And ProjectLoginActive) Then
            Try
                Using db As New ICExProject()
                    Dim result = (From i In db.cs_users Where i.user_name = userName Select i).SingleOrDefault()
                    If (result IsNot Nothing) Then
                        Return 0
                    End If
                    Dim newUser As New cs_users()
                    newUser.user_name = userName
                    newUser.user_pass = GetHashPass(userPass)
                    newUser.user_createdate = ProjectDate
                    newUser.user_lastlogin = ProjectDate
                    newUser.user_enddate = ProjectDate
                    newUser.user_online = False
                    newUser.user_hdd = GetHashPass(GetSerialNumber())
                    db.cs_users.Add(newUser)
                    db.SaveChanges()
                    Return 1
                End Using
            Catch ex As Exception
                Return 2
            End Try
        End If
        ProjectServerStatu = False
        Return 2
    End Function
    Public Function LoginUser(ByVal userName As String, ByVal userPass As String) As Integer
        GetProjectSettings()
        If (CheckModel() And ProjectLoginActive) Then
            Try
                Using db As New ICExProject()
                    Dim passHash As String = GetHashPass(userPass)
                    Dim loginResult = (From i In db.cs_users Where i.user_name = userName And i.user_pass = passHash Select i).SingleOrDefault()
                    If (loginResult IsNot Nothing) Then
                        userServerId = ProjectUserId
                        ProjectUserId = loginResult.user_id
                        ProjectUserEndDate = loginResult.user_enddate
                        Dim userSerial As String = GetUserSerailNumber()
                        If (GetUserOnline() = True) Then
                            If (userSerial = GetHashPass(GetSerialNumber())) Then
                                ProjectRestDate = ProjectUserEndDate - ProjectDate
                                If (ProjectRestDate.TotalHours <= 0) Then Return 1
                                SetOnlineUser(ProjectUserId, True)
                                Return 0
                            Else
                                Return 3
                            End If
                        Else
                            ProjectRestDate = ProjectUserEndDate - ProjectDate
                            If (ProjectRestDate.TotalHours <= 0) Then Return 1
                            SetOnlineUser(ProjectUserId, True)
                            Return 0
                        End If
                    Else
                        Return 2
                    End If
                End Using
            Catch ex As Exception
                Return 4
            End Try
        End If
        ProjectServerStatu = False
        Return 4
    End Function
    Public Function AddKeyToUser(ByVal userName As String, ByVal keyCode As String) As Integer
        GetProjectSettings()
        If (ProjectUserId = 0) Then
            goTxt("Giriş yapmadan key yükleyemezsiniz.", "Lütfen bota giriş yapınız.")
            Return 5
        End If
        If (CheckModel() And ProjectLoginActive) Then
            Try
                Using db As New ICExProject()
                    Dim keyResult = (From i In db.cs_keys Where i.key_code = keyCode Select i).SingleOrDefault()
                    If (keyResult Is Nothing) Then
                        Return 4
                    End If
                    If (keyResult.user_id <> 0) Then
                        Return 3 ' Kullanılmış key
                    End If
                    If (keyResult.key_statu = False) Then
                        Return 2 ' Key aktif değil
                    End If
                    Dim userResult = (From i In db.cs_users Where i.user_id = ProjectUserId Select i).SingleOrDefault()
                    If (userResult IsNot Nothing And keyResult.key_statu And keyResult.user_id = 0) Then
                        ProjectUserEndDate = userResult.user_enddate.AddDays(keyResult.key_day)
                        ProjectRestDate = ProjectUserEndDate - ProjectDate
                        userResult.user_enddate = ProjectUserEndDate
                        keyResult.user_id = ProjectUserId
                        keyResult.key_statu = True
                        db.SaveChanges()
                        Return 1 'Key ok
                    End If
                End Using
            Catch ex As Exception
                Return 5
            End Try
        End If
        ProjectServerStatu = False
        Return 5
    End Function
    Public Function GetUserOnline() As Boolean
        Try
            Using db As New ICExProject()
                Dim result = (From i In db.cs_users Where i.user_id = ProjectUserId Select i).SingleOrDefault()
                If (result IsNot Nothing) Then
                    Return result.user_online
                End If
            End Using
        Catch ex As Exception
            End
        End Try
        Return False
    End Function
    Public Sub SetOnlineUser(ByVal userId As Integer, ByVal userStatu As Boolean)
        Try
            If (userId <> 0) Then
                Using db As New ICExProject()
                    Dim result = (From i In db.cs_users Where i.user_id = userId Select i).SingleOrDefault()
                    If (result IsNot Nothing) Then
                        result.user_hdd = GetHashPass(GetSerialNumber())
                        result.user_online = userStatu
                        db.SaveChanges()
                    End If
                End Using
            End If
        Catch ex As Exception
            End
        End Try
    End Sub
    Public Function GetUserSerailNumber() As String
        Try
            Using db As New ICExProject()
                Return (From i In db.cs_users Where i.user_id = ProjectUserId Select i.user_hdd).SingleOrDefault()
            End Using
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Module
