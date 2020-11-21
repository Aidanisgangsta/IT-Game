Imports System.IO

Module PlayUserQuiz
    Dim NameFromTxt As String = File.ReadAllText(Environment.CurrentDirectory & "\TempUsername.txt")
    Dim UserQuizzes As String() = File.ReadAllLines(Environment.CurrentDirectory & "\QuizNames.txt")
    Dim TotalUserQuizzes As Integer = UserQuizzes.Length
    Dim QuizToPlay As String

    Const MINSCORE As Integer = 50
    Const MAXSCORE As Integer = 1000

    Sub PickQuiz(ByVal TotalUserQuizzes As Integer, ByRef QuizToPlay As String)
        Dim UserInput As Integer
        Dim ExCatcher As Boolean = True
        While ExCatcher
            If TotalUserQuizzes = 0 Then
                Console.WriteLine("There are no user quizzes available to play")
                ExCatcher = False
                Referenced_Main()
            Else
                Console.WriteLine($"Which quiz would you like to play? (1-{TotalUserQuizzes})") 'Here is a number input. Changes depending on how many quizzes currently exist
                Try
                    UserInput = Console.ReadLine()
                Catch ex As Exception
                    'Checks if user input is a integer
                    Console.WriteLine(vbCrLf & "Please enter a valid option" & vbCrLf)
                    ExCatcher = False
                    PickQuiz(TotalUserQuizzes, QuizToPlay)
                Finally
                    'Catches whether user input is within given range
                    If UserInput <= 0 Or UserInput >= TotalUserQuizzes + 1 Then
                        Console.WriteLine(vbCrLf & "Please enter a number between the given range" & vbCrLf)
                    Else
                        ExCatcher = False
                        Console.Clear()
                        QuizToPlay = UserQuizzes(UserInput - 1)
                        Main()
                    End If
                End Try
            End If
        End While
    End Sub

    'Clears previous key presses by the user
    Sub ClearBuffer()
        While Console.KeyAvailable
            Console.ReadKey(True)
        End While
    End Sub

    Sub Quiz_Start_Prompt()
        Dim Start_Conf As String
        Console.WriteLine(vbCrLf & "Press 's' to start the quiz")
        Start_Conf = Console.ReadLine()
        Start_Conf = LCase(Start_Conf) 'Converts Start_Conf to lower case'
        If Start_Conf = "s" Then
            Console.Clear()
        Else
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.Red
            Console.Write("Aborting Quiz")
            Threading.Thread.Sleep(500)
            Console.Write(".")
            Threading.Thread.Sleep(500)
            Console.Write(".")
            Threading.Thread.Sleep(500)
            Console.Write(".")
            Threading.Thread.Sleep(500)
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.White
            Referenced_Main()
        End If
    End Sub

    Sub ReadQuiz(ByVal QuizToPlay As String, ByVal MINSCORE As Integer, ByVal MAXSCORE As Integer)
        Dim lineCount = File.ReadAllLines(Environment.CurrentDirectory & "\UserQuizes" & $"\{QuizToPlay}" & $"\{QuizToPlay}.txt").Length
        Dim QuizLocation As String = Environment.CurrentDirectory & "\UserQuizes" & $"\{QuizToPlay}" & $"\{QuizToPlay}.txt"
        Dim Question As String
        Dim Answer As String
        Dim UserAnswer As String
        Dim timeElapsed As Single
        Dim n As Integer = 1
        Dim NewPoints As Integer
        Dim TotalScore As Integer = 0
        Dim Correct As Integer = 0
        Dim QuizLength As Integer = 0

        For Each line In File.ReadAllLines(QuizLocation)
            If n = 1 Then
                Question = line
                n += 1
                QuizLength += 1
            ElseIf n = 2 Then
                Dim watch As Stopwatch = Stopwatch.StartNew() 'Creates new stopwatch instance
                Answer = line
                n = 1
#Disable Warning BC42104 ' Variable is used before it has been assigned a value
                Console.WriteLine($"What is the answer to {Question}?")
#Enable Warning BC42104 ' Variable is used before it has been assigned a value
                UserAnswer = Console.ReadLine()

                If Answer = UserAnswer Then 'Says it has not been assigned but it has
                    watch.Stop()

                    timeElapsed = Math.Round(watch.Elapsed.TotalMilliseconds)
                    NewPoints = (MAXSCORE - (1 + timeElapsed) / 10) + (7 * (1 + timeElapsed) / 100) 'Using default score incriment of 7 so the difficult is hard coded
                    If NewPoints < MINSCORE Then
                        NewPoints = MINSCORE
                    End If

                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine($"Congratulations you got the correct answer and gained {NewPoints} points")
                    Console.ForegroundColor = ConsoleColor.White
                    Threading.Thread.Sleep(2500)
                    ClearBuffer()
                    Console.Clear()
                    Correct += 1
                    TotalScore += NewPoints
                Else
                    watch.Stop()
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine($"Sorry, you got the wrong answer. The correct answer was {Answer}")
                    Console.ForegroundColor = ConsoleColor.White
                    Threading.Thread.Sleep(2500)
                    ClearBuffer()
                    Console.Clear()
                End If

            End If
        Next
        Dim MaxTotalScore As Integer = lineCount * MAXSCORE
        Console.WriteLine("Congratulations, you got {0}/{1} correct and got {2}/{3} points!", Correct, QuizLength, TotalScore, lineCount / 2 * 1000)
        Threading.Thread.Sleep(3000)
        Console.Clear()

        Dim UserScoreDetails As String = $"{NameFromTxt},{Correct},{TotalScore}" 'Creates string file to write to txt file
        Dim ExCatcher As Boolean = True
        'Checks to prevent throwing and error
        While ExCatcher
            Try
                Using writer As New StreamWriter(Environment.CurrentDirectory & "\UserQuizes" & $"\{QuizToPlay}" & $"\{QuizToPlay}_Scores.txt", True)
                    writer.WriteLine(UserScoreDetails)
                    writer.Close()
                    ExCatcher = False
                End Using
            Catch
                ExCatcher = True
            End Try
        End While
    End Sub

    Sub Leaderboard()
        Console.WriteLine("Top 10 positions are as follows (Name, Correct, Score):" & vbCrLf)

        Dim lineCount = File.ReadAllLines(Environment.CurrentDirectory & "\UserQuizes" & $"\{QuizToPlay}" & $"\{QuizToPlay}_Scores.txt").Length - 1
        Dim ScoreLocation As String = Environment.CurrentDirectory & "\UserQuizes" & $"\{QuizToPlay}" & $"\{QuizToPlay}_Scores.txt"
        Dim AllScore(lineCount, 2) As String
        Dim AllScoreSorted(lineCount, 2) As String
        Dim Position As Integer = 0
        Dim ScoresSorted(lineCount) As Integer
        Const TOPSCORES As Integer = 9

        For Each Line In File.ReadAllLines(ScoreLocation)
            Dim IndivScores = Line.Split(",")
            ScoresSorted(Position) = IndivScores(2)

            'Adds each user information to two dimensional array
            For i As Integer = 0 To IndivScores.Length - 1
                AllScore(Position, i) = IndivScores(i)
            Next
            Position += 1
        Next

        'Sorts Scores into descending order
        Array.Sort(ScoresSorted)
        Array.Reverse(ScoresSorted)

        'Sets the sorted scores into the scores column in the sorted leaderboard
        For i As Integer = 0 To lineCount
            AllScoreSorted(i, 2) = ScoresSorted(i)
        Next

        'Finds corresponding values in the ScoresSorted array and sorts them based on those values score
        For i As Integer = 0 To lineCount
            For j As Integer = 0 To lineCount
                If AllScore(i, 2) = ScoresSorted(j) Then 'Adds names and correct elements to respective score
                    AllScoreSorted(j, 0) = AllScore(i, 0)
                    AllScoreSorted(j, 1) = AllScore(i, 1)
                End If
            Next
        Next

        'Writes the top 10 into console
        Dim Placing As Integer = 1
        If lineCount = -1 Then
            Console.WriteLine("There are no current placings on the leaderboard")
        Else
            For i As Integer = 0 To lineCount
                If i > TOPSCORES Then
                    Exit For
                End If
                Console.WriteLine($"{Placing}: {AllScoreSorted(i, 0)}, {AllScoreSorted(i, 1)}, {AllScoreSorted(i, 2)}")
                Placing += 1
            Next
        End If
        Threading.Thread.Sleep(3000)
        ClearBuffer()
    End Sub

    Sub Main()
        Leaderboard()
        Quiz_Start_Prompt()
        ReadQuiz(QuizToPlay, MINSCORE, MAXSCORE)
    End Sub

    Sub Reference()
        'For referncing from MainPage.vb
        PickQuiz(TotalUserQuizzes, QuizToPlay)
    End Sub

End Module