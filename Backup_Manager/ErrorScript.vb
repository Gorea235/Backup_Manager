Module ErrorScript
    'The function to add to the 'Application Events' file:
    'Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
    '    CatchError(sender, e)
    'End Sub
    '(Do Ctrl E, U to uncomment the lines)
    Private ES_Xdoc As XDocument
    Private ES_DateToday As String
    Private ES_Dates(2) As String
    Private ES_Times(2) As String

    Sub CatchError(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs)
        MessageBox.Show("An error has occured in the app. Please send the file located at the launching directory to the delevopers.", "Error has occured.", Nothing, MessageBoxIcon.Error)
        ES_Xdoc = New XDocument
        ES_Xdoc.Add(New XElement("RootError", _
                            New XElement("AppInfo", _
                                New XElement("ProductName", Application.ProductName), _
                                New XElement("CompanyName", Application.CompanyName), _
                                New XElement("ProductVersion", Application.ProductVersion), _
                                New XElement("LocationOnSystem", System.AppDomain.CurrentDomain.BaseDirectory)), _
                            New XElement("ErrorInfo", _
                                New XElement("Message", e.Exception.Message), _
                                New XElement("Source", e.Exception.Source), _
                                New XElement("StackTrace", e.Exception.StackTrace), _
                                New XElement("TargetSite", e.Exception.TargetSite), _
                                New XElement("Data", e.Exception.Data))))
        ES_Dates = Split(Microsoft.VisualBasic.Left(Now, 10), "/")
        ES_Times = Split(Microsoft.VisualBasic.Right(Now, 10), ":")
        ES_DateToday = ES_Dates(0) & "-" & ES_Dates(1) & "-" & ES_Dates(2) & " " & ES_Times(0) & "." & ES_Times(1) & "." & ES_Times(2)
        Try
            ES_Xdoc.Save(System.AppDomain.CurrentDomain.BaseDirectory & "crash error " & ES_DateToday & ".crashxml")
            MessageBox.Show("Created test error successfully. Test crash file name: " & "crash error " & ES_DateToday & ".crashxml", "Success", Nothing, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        End
    End Sub
End Module
