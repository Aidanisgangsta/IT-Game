Imports System.IO

Module GeographyQuiz
    Dim Correct As Integer = 0
    Dim TotalScore As Integer = 0
    Const MaxScore As Integer = 1000
    Const MinScore As Integer = 50

    Dim Questions(9) As String
    Dim Answers(9) As String
    Dim WrongAnswers(9, 3) As String

    Dim NameFromTxt As String = File.ReadAllText(Environment.CurrentDirectory & "\TempUsername.txt") 'Retrives temporary name from text'

    Sub QuestionsAdd(ByRef Questions As Array, ByRef WrongAnswers As Array, ByRef Answers As Array)
        Questions(0) = "What is the world's largest continent?"
        Questions(1) = "What is the smallest country in the world?"
        Questions(2) = "What is the only sea on Earth with no coastline?"
        Questions(3) = "Cuba is less than a hundred miles away from what U.S. state?"
        Questions(4) = "Which country has the most active volcanoes?"
        Questions(5) = "Which lake is the worlds largest lake?"
        Questions(6) = "What country has the longest coastline in the world?"
        Questions(7) = "What is the largest country in the world?"
        Questions(8) = "What is the tallest mountain?"
        Questions(9) = "What is the capital of Australia?"

        'Correct answers
        Answers(0) = "Asia"
        Answers(1) = "The Vatican"
        Answers(2) = "Sargasso Sea"
        Answers(3) = "Florida"
        Answers(4) = "Indonesia"
        Answers(5) = "Caspian Sea"
        Answers(6) = "Canada"
        Answers(7) = "Russia"
        Answers(8) = "Mount Kia"
        Answers(9) = "Canberra"

        'Codes in wrong answers and they will be randomly selected
        WrongAnswers(0, 0) = "Antarctica"
        WrongAnswers(0, 1) = "South America"
        WrongAnswers(0, 2) = "North America"
        WrongAnswers(0, 3) = "Australia"

        WrongAnswers(1, 0) = "Monaco"
        WrongAnswers(1, 1) = "Rome"
        WrongAnswers(1, 2) = "San Marino"
        WrongAnswers(1, 3) = "Malta"

        WrongAnswers(2, 0) = "Mediterranean Sea"
        WrongAnswers(2, 1) = "Caribbean Sea"
        WrongAnswers(2, 2) = "White Sea"
        WrongAnswers(2, 3) = "Dead Sea"

        WrongAnswers(3, 0) = "Hawaii"
        WrongAnswers(3, 1) = "California"
        WrongAnswers(3, 2) = "Alaska"
        WrongAnswers(3, 3) = "Alabama"

        WrongAnswers(4, 0) = "Iceland"
        WrongAnswers(4, 1) = "USA"
        WrongAnswers(4, 2) = "New Zealand"
        WrongAnswers(4, 3) = "Mongolia"

        WrongAnswers(5, 0) = "Lake Michigan-Huron"
        WrongAnswers(5, 1) = "Lake Taupo"
        WrongAnswers(5, 2) = "Lake Superior"
        WrongAnswers(5, 3) = "Aral Sea"

        WrongAnswers(6, 0) = "Russia"
        WrongAnswers(6, 1) = "China"
        WrongAnswers(6, 2) = "Indonesia"
        WrongAnswers(6, 3) = "Greenland"

        WrongAnswers(7, 0) = "Canada"
        WrongAnswers(7, 1) = "China"
        WrongAnswers(7, 2) = "USA"
        WrongAnswers(7, 3) = "Australia"

        WrongAnswers(8, 0) = "Mount Everest"
        WrongAnswers(8, 1) = "K2"
        WrongAnswers(8, 2) = "Mount Kilimanjaro"
        WrongAnswers(8, 3) = "Mount Kangchenjunga"

        WrongAnswers(9, 0) = "New South Wales"
        WrongAnswers(9, 1) = "Tasmania"
        WrongAnswers(9, 2) = "Queensland"
        WrongAnswers(9, 3) = "Victoria"
    End Sub

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

    Sub ClearBuffer()
        While Console.KeyAvailable
            Console.ReadKey(True)
        End While
    End Sub

    Sub AskQuestions(ByVal MaxScore As Integer, ByVal MinScore As Integer, ByRef Correct As Integer, ByRef TotalScore As Integer)
        Dim NewPoints As Integer
        Dim Ans As Integer
        Dim Positions() As Integer = {0, 1, 2, 3} 'Creates array of all possible answer positions
        'Used for timing how long it takes to answer the question
        Dim timeElapsed As Single

        'Cycles through all the questions and answers
        For i As Integer = 0 To Questions.Length - 1
            Console.Clear()
            Console.WriteLine(Questions(i))

            'Randomises the order of the multi choice answers
            Dim rnd As New Random
            Dim Rand As Integer = rnd.Next(4)
            Dim AnswerAsInt As Integer

            'Writes the potential answers on one line in a specific depending on the random number that is generated
            For j As Integer = 0 To 3
                If Rand = j Then
                    Console.Write($"{j + 1}: {Answers(i)}   ") 'Writes correct answer into random position
                    AnswerAsInt = j + 1
                Else
                    Console.Write($"{j + 1}: {WrongAnswers(i, Positions(j))}    ") 'Writes incorrect answers to console
                End If
            Next
            Console.WriteLine("")
            'Confirms whether the user enters a valid input and within the ragne
            Dim ValidInputChecker As Boolean = True
            Dim watch As Stopwatch
            While ValidInputChecker
                ' Create new Stopwatch instance and starts it
                watch = Stopwatch.StartNew()
                Try
                    Ans = Console.ReadLine()
                    If Ans >= 1 And Ans <= 4 Then
                        ValidInputChecker = False
                        watch.Stop()
                    End If
                Catch ex As Exception
                    Console.WriteLine("Please enter an input between 1 and 4")
                End Try
            End While

            Try
                If Ans = AnswerAsInt Then
                    'Sets timeElapsed to total time taken in miliseconds
#Disable Warning BC42104 ' Variable is used before it has been assigned a value
                    timeElapsed = Math.Round(watch.Elapsed.TotalMilliseconds)
#Enable Warning BC42104 ' Variable is used before it has been assigned a value
                    'Sets NewPoints
                    NewPoints = (MaxScore - (1 + timeElapsed) / 10) + (5 * (1 + timeElapsed) / 100)
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
                    Console.WriteLine("Sorry, the correct answer was {0}({1})", Answers(i), AnswerAsInt)
                    Console.ForegroundColor = ConsoleColor.White
                    watch.Stop()
                    Threading.Thread.Sleep(2500)
                    ClearBuffer()
                End If
            Catch ex As Exception
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("Sorry, the correct answer was {0}({1})", Answers(i), AnswerAsInt)
                Console.ForegroundColor = ConsoleColor.White
                watch.Stop()
                Threading.Thread.Sleep(2500)
                ClearBuffer()
            End Try
        Next
        Console.Clear()
        Console.WriteLine("Congratulations, you got {0}/10 correct and got {1}/10000 points!", Correct, TotalScore)
        Threading.Thread.Sleep(3000)

        'Writes users score to txt file
        Dim UserScoreDetails As String = $"{NameFromTxt},{Correct},{TotalScore}"
        Dim ExCatcher As Boolean = True
        'Checks to prevent throwing and error
        While ExCatcher
            Try
                Using writer As New StreamWriter(Environment.CurrentDirectory & "\GeographyUserScores.txt", True)
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

        Dim lineCount = File.ReadAllLines(Environment.CurrentDirectory & "\GeographyUserScores.txt").Length - 1
        Dim ScoreLocation As String = Environment.CurrentDirectory & "\GeographyUserScores.txt"
        Dim AllScore(lineCount, 2) As String
        Dim AllScoreSorted(lineCount, 2) As String
        Dim Position As Integer = 0
        Dim ScoresSorted(lineCount) As Integer

        For Each Line In File.ReadAllLines(ScoreLocation)
            Dim IndivScores = Line.Split(",")
            ScoresSorted(Position) = IndivScores(2)

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
        QuestionsAdd(Questions, WrongAnswers, Answers)
        Leaderboard()
        Quiz_Start_Prompt()
    End Sub

End Module