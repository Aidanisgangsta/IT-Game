Imports System.IO

Module MainPage
    Dim Name As String
    ReadOnly interval As Integer = 15

    '''<summary>
    '''Writes every out slowly with with a given time delay between each letter. 
    '''Returns: Nothing. 
    '''Takes Parameters: message as String, interval As String
    '''</summary>
    Sub SlowType(message As String, interval As Integer)
        For Each letter In message
            Console.Write(letter)
            Threading.Thread.Sleep(interval)
        Next
        Console.WriteLine("")
    End Sub

    '''<summary>
    '''Prompts user to enter a username. 
    '''Returns: Nothing
    '''Takes Parameters: Name As String
    '''</summary>
    Sub SetUsername(ByRef Name As String)
        SlowType(vbCrLf & "Hello! What would you like your username to be? (Make sure it is 16 or less characters)", interval)
        Name = Console.ReadLine()
        If Name.Length > 16 Then 'Checks length of username
            SlowType(vbCrLf & "Please enter a username that is 16 characters or less", interval)
            SetUsername(Name)
        ElseIf Name = "" Then 'Checks if username is blank
            SlowType(vbCrLf & "Please enter a proper name", interval)
            SetUsername(Name)
        Else
            For Each line As String In File.ReadLines("Usernames.txt") 'Checks whether name exists in txt file
                If line = Name Then
                    SlowType(vbCrLf & "Sorry but that username is taken", interval)
                    SetUsername(Name)
                End If
            Next
        End If
    End Sub

    '''<summary>
    '''Prompts user to confirm their name. 
    '''Returns: Nothing. 
    '''Takes Parameters: Name As String
    '''</summary>
    Sub UsernameConfirm(ByVal Name As String)
        Dim UserConfirmation As String

        SlowType(vbCrLf & $"So would you like your username to be {Name}? (y or n)", interval)
        UserConfirmation = LCase(Console.ReadLine()) 'Converts to user entry to lowercase

        If UserConfirmation = "y" Then
            'Writes username to a temporary file which can be transfered over different scripts
            Dim ExCatcher As Boolean = True
            While ExCatcher
                Try
                    Using writer As New StreamWriter(Environment.CurrentDirectory & "\TempUsername.txt", False) 'Finds txt file and allows existing text to be overwrititen
                        writer.Write(Name) 'Overwrites name to txt file
                        writer.Close()
                        ExCatcher = False
                    End Using
                Catch
                    ExCatcher = True
                End Try
            End While
            'Adds username to permanent txt file
            Dim ExCatcher2 As Boolean = True
            While ExCatcher2
                Try
                    Using writer As New StreamWriter(Environment.CurrentDirectory & "\Usernames.txt", True) 'Finds txt file and allows name to be appened to end of txt file
                        writer.WriteLine(Name) 'Overwrites name to txt file
                        writer.Close()
                        ExCatcher2 = False
                    End Using
                Catch
                    ExCatcher2 = True
                End Try
            End While
            SlowType(vbCrLf & $"Hello, {Name}!" & vbCrLf, interval)
        ElseIf UserConfirmation = "n" Then
            ReferenceMakeUser()
        Else
            SlowType(vbCrLf & "Please enter a valid option", interval)
            UsernameConfirm(Name)
        End If
    End Sub

    '''<summary>
    '''Prompts user to pick a quiz. 
    '''Returns: Nothing. 
    '''Takes Parameters: Nothing.
    '''</summary>
    Sub Pick_Quiz()

        Dim quiz_choice As String

        While True
            Console.WriteLine(
"Quiz Menu:

'm' to play the math quiz
'g' to play the general knowledge quiz
'p' to play a geography quiz 
'u' to play a user quiz
'q' to return to main menu"
)
            quiz_choice = LCase(Console.ReadLine())

            If quiz_choice = "q" Then 'Return to main menu
                Console.Clear()
                Exit While
            ElseIf quiz_choice = "m" Then 'Math quiz
                Console.Clear()
                MathQuiz.Main()
            ElseIf quiz_choice = "g" Then 'General knowledge
                Console.Clear()
                GeneralKnowledgeQuiz.Main()
            ElseIf quiz_choice = "p" Then 'Geography
                Console.Clear()
                GeographyQuiz.Main()
            ElseIf quiz_choice = "u" Then 'User quizzes
                Console.Clear()
                View_All_Quizes()
                Reference()
            Else
                Console.WriteLine("Please enter a valid option" & vbCrLf)
            End If

        End While

    End Sub

    '''<summary>
    '''Prints all user made quizzes. 
    '''Returns: Nothing. 
    '''Takes Parameters: Nothing.
    '''</summary>
    Sub View_All_Quizes()
        Dim QuizNum As Integer = 0
        Console.WriteLine("Here are all the user quizzes: " & vbCrLf)

        'Iterates through all items in txt file
        For Each Line In File.ReadAllLines("QuizDetails.txt")
            QuizNum += 1
            Console.WriteLine($"{QuizNum}: {Line}" & vbCrLf)
        Next
    End Sub

    '''<summary>
    '''Displays a menu to user showing them options. 
    '''Returns: Nothing. 
    '''Takes Parameters: Nothing.
    '''</summary>
    Sub Menu()
        Dim menu_option As String

        While True
            Console.WriteLine("Main Menu:")
            SlowType(
"'a' to add your own quiz
'p' to play an existing quiz
'v' to view all community quizes
'q' to quit", interval
)
            menu_option = LCase(Console.ReadLine()) 'Converts user entry to all lowercase

            If menu_option = "q" Then
                Exit While
            ElseIf menu_option = "a" Then
                Console.Clear()
                AddQuiz.Main()
            ElseIf menu_option = "p" Then
                Console.Clear()
                Pick_Quiz()
            ElseIf menu_option = "v" Then
                Console.Clear()
                View_All_Quizes()
            Else
                Console.WriteLine(vbCrLf & "Please enter a valid option")
            End If

        End While
    End Sub

    '''<summary>
    '''Used to stop looping when creating username. 
    '''Returns: Name as String. 
    '''Takes Parameters: Name As String
    '''</summary>
    Sub ReferenceMakeUser()
        SetUsername(Name)
        UsernameConfirm(Name)
    End Sub

    Sub Main()
        Start_Up()
        SetUsername(Name)
        UsernameConfirm(Name)
        Menu()
        Shut_Down()
    End Sub

    '''<summary>
    '''Subroutine for other scripts to reference without going through startup processes. 
    '''Returns: Nothing. 
    '''Takes Parameters: Nothing.
    '''</summary>
    Sub Referenced_Main()
        Menu()
        Shut_Down()
    End Sub

End Module