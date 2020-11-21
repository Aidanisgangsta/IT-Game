Imports System.IO

Module AddQuiz
    ReadOnly NonValid() As String = {"CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", "", " ", "<", ">", ":", """", "/", "\", "\", "|", "?", "*"}

    Dim QuizName As String
    Dim QuizLength As Integer = 0
    Dim NameFromTxt As String = File.ReadAllText(Environment.CurrentDirectory & "\TempUsername.txt") 'Gets users name from txt file
    Dim QuizFileDirectory As String

    Const MAXQUIZLENGTH As Integer = 50

    '''<summary>
    '''Prompts user to enter a username.
    '''Returns: Name as String.
    '''Takes Parameters: Name As String
    '''</summary>
    Sub CreateQuiz(ByRef QuizName As String)
        Console.WriteLine("Hello! What would you like your quiz to be called? (Make sure it is under 16 characters)")
        QuizName = Console.ReadLine()
        If QuizName.Length > 16 Then 'Checks length of quizname
            Console.WriteLine(vbCrLf & "Please enter a name that is 16 characters or less" & vbCrLf)
            CreateQuiz(QuizName)
        ElseIf NonValid.Contains(QuizName) Then 'Checks if quizname is invalid
            Console.WriteLine(vbCrLf & "Please enter a valid name" & vbCrLf)
            CreateQuiz(QuizName)
        Else
            If Directory.Exists(Environment.CurrentDirectory & "/UserQuizes" & $"/{QuizName}") Then
                Console.WriteLine(vbCrLf & "Sorry but that username is taken" & vbCrLf)
                CreateQuiz(QuizName)
            End If
        End If
    End Sub

    Sub QuizName_Confirm(ByVal QuizName As String, ByRef QuizFileDirectory As String)
        Dim QuizConfirmation As String

        Console.WriteLine(vbCrLf & "So would you like your quiz to be named {0}? (y or n)", QuizName)
        QuizConfirmation = LCase(Console.ReadLine()) 'Converts to user entry to lowercase
        If QuizConfirmation = "y" Then
            'Writes quiz name to txt file
            Dim ExCatcher3 As Boolean = True
            While ExCatcher3
                Try
                    Using writer As New StreamWriter(Environment.CurrentDirectory & "\QuizNames.txt", True)
                        writer.WriteLine(QuizName)
                        writer.Close()
                        ExCatcher3 = False
                    End Using
                Catch
                    ExCatcher3 = True
                End Try
            End While
            QuizFileDirectory = Environment.CurrentDirectory & "/UserQuizes" & $"/{QuizName}"
            'Creates directory and all needed txt files
            Directory.CreateDirectory(QuizFileDirectory)
            File.Create(QuizFileDirectory & $"/{QuizName}.txt")
            File.Create(QuizFileDirectory & $"/{QuizName}_Scores.txt")
            Console.WriteLine(vbCrLf & "Okay, you have created a quiz named: {0}!" & vbCrLf, QuizName)
            Threading.Thread.Sleep(2500)
        ElseIf QuizConfirmation = "n" Then
            Console.WriteLine("")
            ReferenceMakeQuiz()
        Else
            Console.WriteLine(vbCrLf & "Please enter a valid option")
            QuizName_Confirm(QuizName, QuizFileDirectory)
        End If
    End Sub

    Sub TotalQuestions(ByRef QuizLength As Integer, ByVal MAXQUIZLENGTH As Integer)
        Dim MNQ_Confirmation As Boolean = True 'MNQ = Make New Quiz'
        'Asks the amount of questions you want'
        Console.Clear()
        While MNQ_Confirmation
            Console.WriteLine($"How many questions would you like in your quiz? (1-{MAXQUIZLENGTH}) ") 'Number input
            Try
                QuizLength = Console.ReadLine()
                If QuizLength <= 0 Or QuizLength > MAXQUIZLENGTH Then
                    Console.WriteLine(vbCrLf & "Please enter an reasonable/valid number" & vbCrLf)
                Else
                    MNQ_Confirmation = False
                End If
            Catch ex As Exception
                Console.WriteLine(vbCrLf & "Please enter an integer" & vbCrLf)
            End Try
        End While
    End Sub

    '''<summary>
    '''Asks user for questions and answers they want in their quiz.
    '''Returns: Nothing.
    '''Takes Parameters: Quiz_Length As Integer.
    '''</summary>
    Sub CreateQuestions(ByVal Quiz_Length As Integer)
        Dim New_Quiz_Question As String
        Dim New_Quiz_Answer As String

        'Asks the user for the previously specified number of questions and answers to write to the txt file'
        For i As Integer = 1 To Quiz_Length
            Dim ExCatcher As Boolean = True

            Console.Clear()
            Console.WriteLine("Question {0} to add: ", i)
            New_Quiz_Question = CStr(Console.ReadLine()) 'Converts to string'
            Console.WriteLine("Answer to Question {0}: ", i)
            New_Quiz_Answer = CStr(Console.ReadLine()) 'Converts to string'

            If i = 1 Then
                Console.WriteLine("Please wait for you quiz to initialise before adding more questions")
            End If

            Dim Q_n_A_AsString = $"{New_Quiz_Question}" & vbCrLf & $"{New_Quiz_Answer}" 'Converts user enteries to concatenated string to append to txt file'
            'Prevents System.IO.IO.Exception (Throws this A LOT of times the first time)
            While ExCatcher
                Try
                    Using writer As New StreamWriter(Environment.CurrentDirectory & "\UserQuizes" & $"\{QuizName}" & $"\{QuizName}.txt", True)
                        writer.WriteLine(Q_n_A_AsString)
                        writer.Close()
                        ExCatcher = False
                    End Using
                Catch
                    ExCatcher = True
                End Try
            End While

            Console.ForegroundColor = ConsoleColor.Green
            If i = 1 Then
                Console.WriteLine("Quiz Initialised!")
                Threading.Thread.Sleep(1000)
            End If
            Console.ForegroundColor = ConsoleColor.White
        Next
        Console.Clear()
        Console.WriteLine($"Thank you for creating your quiz, {NameFromTxt}")

        'Writes a short description of the quiz on the first text line and to a txt file'
        Dim ExCatcher2 As Boolean = True
        Dim QuizDescription As String = $"{QuizName} by {NameFromTxt} ({Quiz_Length} questions long)" 'Creates description'
        While ExCatcher2
            Try
                Using writer As New StreamWriter(Environment.CurrentDirectory & "\QuizDetails.txt", True)
                    writer.WriteLine(QuizDescription)
                    writer.Close()
                    ExCatcher2 = False
                End Using
            Catch
                ExCatcher2 = True
            End Try
        End While
        Console.Clear()
    End Sub

    'Used to stop a looping error when referencing
    Sub ReferenceMakeQuiz()
        CreateQuiz(QuizName)
        QuizName_Confirm(QuizName, QuizFileDirectory)
    End Sub

    Sub Main()
        CreateQuiz(QuizName)
        QuizName_Confirm(QuizName, QuizFileDirectory)
        TotalQuestions(QuizLength, MAXQUIZLENGTH)
        CreateQuestions(QuizLength)
    End Sub

End Module