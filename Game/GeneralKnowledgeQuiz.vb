Imports System.IO

Module GeneralKnowledgeQuiz
    Dim Correct As Integer = 0
    Dim TotalScore As Integer = 0
    Const MaxScore As Integer = 1000
    Const MinScore As Integer = 50

    Dim Questions(9) As String
    Dim Answers(9) As String
    Dim WrongAnswers(9, 3) As String

    Dim NameFromTxt As String = File.ReadAllText(Environment.CurrentDirectory & "\TempUsername.txt") 'Retrives temporary name from text'

    Sub QuestionsAdd(ByRef Questions As Array, ByRef WrongAnswers As Array, ByRef Answers As Array)
        Questions(0) = "Who is the All Blacks captain?"
        Questions(1) = "What is the only letter not used on the periodic table?"
        Questions(2) = "Who is the highest paid actor?"
        Questions(3) = "What year did WW2 start?"
        Questions(4) = "How big is a bakers dozen?"
        Questions(5) = "What is Obamas first name?"
        Questions(6) = "What is the chemcial formula for table salt?"
        Questions(7) = "How long is one astronomical unit (in metres)?"
        Questions(8) = "Which singer was known amongst other things as 'The King of Pop'?"
        Questions(9) = "What is the average temperature of space?"

        'Correct answers
        Answers(0) = "Sam Cane"
        Answers(1) = "J"
        Answers(2) = "Dwayne Johnson"
        Answers(3) = "1939"
        Answers(4) = "13"
        Answers(5) = "Barack"
        Answers(6) = "NaCl"
        Answers(7) = "149 597 870 700"
        Answers(8) = "Michael Jackson"
        Answers(9) = "2.7K"

        'Codes in wrong answers and they will be randomly selected
        WrongAnswers(0, 0) = "Sir Richie McCaw"
        WrongAnswers(0, 1) = "Kieran Read"
        WrongAnswers(0, 2) = "Beauden Barrett"
        WrongAnswers(0, 3) = "Dan Carter"

        WrongAnswers(1, 0) = "Z"
        WrongAnswers(1, 1) = "Q"
        WrongAnswers(1, 2) = "X"
        WrongAnswers(1, 3) = "V"

        WrongAnswers(2, 0) = "Chris Hemsworth"
        WrongAnswers(2, 1) = "Robert Downey Jr"
        WrongAnswers(2, 2) = "Tom Cruise"
        WrongAnswers(2, 3) = "Shah Rukh Khan"

        WrongAnswers(3, 0) = "1945"
        WrongAnswers(3, 1) = "1914"
        WrongAnswers(3, 2) = "1918"
        WrongAnswers(3, 3) = "1947"

        WrongAnswers(4, 0) = "12"
        WrongAnswers(4, 1) = "11"
        WrongAnswers(4, 2) = "14"
        WrongAnswers(4, 3) = "6"

        WrongAnswers(5, 0) = "Obama"
        WrongAnswers(5, 1) = "Barack"
        WrongAnswers(5, 2) = "No one knows"
        WrongAnswers(5, 3) = "44th U.S. President"

        WrongAnswers(6, 0) = "ClNa"
        WrongAnswers(6, 1) = "Salt"
        WrongAnswers(6, 2) = "Na2Cl3"
        WrongAnswers(6, 3) = "MgCl"

        WrongAnswers(7, 0) = "149 597 870"
        WrongAnswers(7, 1) = "1"
        WrongAnswers(7, 2) = "100 000 000 000"
        WrongAnswers(7, 3) = "384 400"

        WrongAnswers(8, 0) = "Elvis Presley"
        WrongAnswers(8, 1) = "Eminem"
        WrongAnswers(8, 2) = "Freddie Mercury"
        WrongAnswers(8, 3) = "Jon Bon Jovi"

        WrongAnswers(9, 0) = "100C"
        WrongAnswers(9, 1) = "98.4K"
        WrongAnswers(9, 2) = "0K"
        WrongAnswers(9, 3) = "0C"
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
                Using writer As New StreamWriter(Environment.CurrentDirectory & "\GNUserScores.txt", True)
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

        Dim lineCount = File.ReadAllLines(Environment.CurrentDirectory & "\GNUserScores.txt").Length - 1
        Dim ScoreLocation As String = Environment.CurrentDirectory & "\GNUserScores.txt"
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

        'Writes leaderboard into console
        Dim Placing As Integer = 1
        If lineCount = -1 Then 'Checks if there is 0 scores available
            Console.WriteLine("There are no current scores available for this quiz" & vbCrLf)
        Else 'Writes top 10 scores along side their respective names and correct score
            For i As Integer = 0 To lineCount
                If i > 9 Then
                    Exit For
                End If
                Console.WriteLine($"{Placing}: {AllScoreSorted(i, 0)}, {AllScoreSorted(i, 1)}, {AllScoreSorted(i, 2)}")
                Placing += 1
            Next
            Console.WriteLine("")
        End If

    End Sub

    Sub Main()
        QuestionsAdd(Questions, WrongAnswers, Answers)
        Leaderboard()
        Quiz_Start_Prompt()
    End Sub

End Module