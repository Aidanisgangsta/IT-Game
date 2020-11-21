Imports System.IO

Module MathQuiz
    Dim Correct As Integer = 0
    Dim TotalScore As Integer = 0
    Const MaxScore As Integer = 1000
    Const MinScore As Integer = 50

    ReadOnly Questions As String() = {"12+23", "65+34", "65-32", "71-42", "6*12", "11*12", "13*14", "15*12", "6^2", "12^2", "14^2", "21^2", "5^3", "6^3", "8^3", "(6*4)/2", "(6*3)(2^3)", "(6^2)*3^2", "(6-3*2)-3(2^3)", "(3!)-(2^0)+(0!*5^2)"}
    ReadOnly Answers As Integer() = {35, 99, 33, 29, 72, 132, 182, 180, 36, 144, 196, 441, 125, 216, 512, 12, 144, 324, -24, 30}
    Dim NameFromTxt As String = File.ReadAllText(Environment.CurrentDirectory & "\TempUsername.txt") 'Retrives temporary name from text'

    '''<summary>
    '''Prompts user to start quiz. 
    '''Returns: Nothing. 
    '''Takes Parameters: Nothing.
    '''</summary>
    Sub Quiz_Start_Prompt()
        Dim Start_Conf As String
        Console.WriteLine("Press 's' to start the quiz")
        Start_Conf = Console.ReadLine()
        Start_Conf = LCase(Start_Conf) 'Converts Start_Conf to lower case'
        If Start_Conf = "s" Then
            AskQuestions(MaxScore, MinScore, Correct, TotalScore)
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

    'Clears any previous key presses from the user
    Sub ClearBuffer()
        While Console.KeyAvailable
            Console.ReadKey(True)
        End While
    End Sub

    '''<summary>
    '''Prompts user to start quiz. 
    '''Returns: Score As . 
    '''Takes Parameters: MaxScore As Integer, MinScore as Integer, Correct as Integer, TotalScore as Integer.
    '''</summary>
    Sub AskQuestions(ByVal MaxScore As Integer, ByVal MinScore As Integer, ByRef Correct As Integer, ByRef TotalScore As Integer)
        Dim NewPoints As Integer
        Dim Ans As String
        'Used for timing how long it takes to answer the question
        Dim timeElapsed As Single

        'Cycles through all the questions and answers
        For i As Integer = 0 To Questions.Length - 1
            Console.Clear()
            Console.WriteLine($"What is the answer to: {Questions(i)}")

            ' Create new Stopwatch instance and starts it
            Dim watch As Stopwatch = Stopwatch.StartNew()
            Ans = Console.ReadLine()
            Try
                If Ans = Answers(i) Then
                    watch.Stop()
                    'Sets timeElapsed to total time taken in miliseconds
                    timeElapsed = Math.Round(watch.Elapsed.TotalMilliseconds)
                    'Sets NewPoints
                    NewPoints = (MaxScore - (1 + timeElapsed) / 10) + (7 * (1 + timeElapsed) / 100)
                    'Makes sure NewPoints cannot be below 50
                    If NewPoints < MinScore Then
                        NewPoints = MinScore
                    End If
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine("Congratulations, you got the correct answer and gained {0} points", NewPoints)
                    Console.ForegroundColor = ConsoleColor.White
                    Correct += 1
                    TotalScore += NewPoints
                    'Waits 3 seconds
                    Threading.Thread.Sleep(3000)
                    ClearBuffer()
                Else
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("Sorry, the correct answer was {0}", Answers(i))
                    Console.ForegroundColor = ConsoleColor.White
                    watch.Stop()
                    Threading.Thread.Sleep(2500)
                    ClearBuffer()
                End If
            Catch ex As Exception
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("Sorry, the correct answer was {0}", Answers(i))
                Console.ForegroundColor = ConsoleColor.White
                watch.Stop()
                Threading.Thread.Sleep(2500)
                ClearBuffer()
            End Try
        Next
        Console.Clear()
        TotalScore /= 2 'Divides TotalScore by 2 to convert it to a number which can be expressed out of 10000
        Console.WriteLine("Congratulations, you got {0}/20 correct and got {1}/10000 points!", Correct, TotalScore)
        Threading.Thread.Sleep(3000)

        'Writes users score to txt file
        Dim UserScoreDetails As String = $"{NameFromTxt},{Correct},{TotalScore}"
        Dim ExCatcher As Boolean = True
        'Checks to prevent throwing and error
        While ExCatcher
            Try
                Using writer As New StreamWriter(Environment.CurrentDirectory & "\MathUserScores.txt", True)
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

        Dim lineCount = File.ReadAllLines(Environment.CurrentDirectory & "\MathUserScores.txt").Length - 1
        Dim ScoreLocation As String = Environment.CurrentDirectory & "\MathUserScores.txt"
        Dim AllScore(lineCount, 2) As String
        Dim AllScoreSorted(lineCount, 2) As String
        Dim Position As Integer = 0
        Dim ScoresSorted(lineCount) As Integer

        'Covnerts text file of all user scores into an array
        For Each Line In File.ReadAllLines(ScoreLocation)
            Dim IndivScores = Line.Split(",")
            ScoresSorted(Position) = IndivScores(2) 'Adds the scores the the score column for every user in 2d array

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
        For i As Integer = 0 To lineCount
            If i > 9 Then
                Exit For
            End If
            Console.WriteLine($"{Placing}: {AllScoreSorted(i, 0)}, {AllScoreSorted(i, 1)}, {AllScoreSorted(i, 2)}")
            Placing += 1
        Next
        Console.WriteLine("")
    End Sub

    Sub Main()
        Leaderboard()
        Quiz_Start_Prompt()
    End Sub

End Module