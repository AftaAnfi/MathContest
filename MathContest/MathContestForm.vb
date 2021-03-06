Option Strict On
Option Explicit On
'Aftanom Anfilofieff
'RCET0265
'Spring 2021
'Math Contest
'https://github.com/AftaAnfi/MathContest.git
Public Class MathContestForm
    Dim correctAnswers As Integer = 0
    Dim totalAnswers As Integer = 0
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub
    Private Sub MathContestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddRadioButton.Checked = True
    End Sub
    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click
        Dim errorString As String
        errorString = ""
        Dim competeable As Boolean
        competeable = True

        'Check StudentAnswer for errors
        CheckIfIntegerInputIsValid(StudentAnswerTextBox, errorString, competeable)

        'Check SecondNumber for errors
        CheckIfIntegerInputIsValid(SecondNumberTextBox, errorString, competeable)

        'Check FirstNumber for errors
        CheckIfIntegerInputIsValid(FirstNumberTextBox, errorString, competeable)

        'Check Grade for errors
        CheckIfIntegerInputIsValid(GradeTextBox, errorString, competeable)

        'Check Age for errors
        CheckIfIntegerInputIsValid(AgeTextBox, errorString, competeable)

        'Check to see if name is empty
        If NameTextBox.Text = "" Then
            errorString &= ($"Name is empty. {vbNewLine}")
            NameTextBox.Focus()
        End If

        'Msg User if there is errors if not, check student answer
        If errorString = "" Then
            If competeable = False Then
                MsgBox($"Student not eligible to compete {vbNewLine}")
            Else
                'check the students answer
                CheckStudentAnswer()
            End If
        Else
            MsgBox(errorString)
        End If
    End Sub
    Sub CheckIfIntegerInputIsValid(ByVal textboxX As TextBox, ByRef errorString As String, ByRef compete As Boolean)
        Dim tempInteger As Integer
        Dim textBoxUserName As String
        textBoxUserName = ""


        textBoxUserName = Replace(textboxX.Name, "TextBox", "")
        If textboxX.Text = "" Then
            errorString &= ($"{textBoxUserName} is empty.{vbNewLine}")
            textboxX.Focus()

        Else
            Try
                tempInteger = CInt(textboxX.Text)

                Select Case textboxX.Name

                    Case AgeTextBox.Name

                        Select Case CInt(textboxX.Text)
                            Case < 7
                                compete = False
                                textboxX.Focus()
                                textboxX.Text = ""
                            Case > 11
                                compete = False
                                textboxX.Focus()
                                textboxX.Text = ""
                        End Select

                    Case GradeTextBox.Name
                        Select Case CInt(textboxX.Text)
                            Case < 1
                                compete = False
                                textboxX.Focus()
                                textboxX.Text = ""
                            Case > 4
                                compete = False
                                textboxX.Focus()
                                textboxX.Text = ""
                        End Select

                    Case Else

                End Select


            Catch ex As Exception
                errorString &= ($"{textBoxUserName} is not an Integer.{vbNewLine}")
                textboxX.Focus()
                textboxX.Text = ""
            End Try
        End If



    End Sub
    Sub CheckStudentAnswer()
        Dim correctAnswer As Integer = 0
        Dim errorState As Boolean = False
        'check which radio button is checked and set correctanswer to appropriate expression
        Select Case True
            Case AddRadioButton.Checked
                correctAnswer = CInt(FirstNumberTextBox.Text) + CInt(SecondNumberTextBox.Text)
            Case SubtractRadioButton.Checked
                correctAnswer = CInt(FirstNumberTextBox.Text) - CInt(SecondNumberTextBox.Text)
            Case MultiplyRadioButton.Checked
                correctAnswer = CInt(FirstNumberTextBox.Text) * CInt(SecondNumberTextBox.Text)
            Case DivideRadioButton.Checked
                Try
                    correctAnswer = CInt(CInt(FirstNumberTextBox.Text) / CInt(SecondNumberTextBox.Text))
                Catch ex As Exception
                    MsgBox("Do not place 2nd number as 0.")
                    errorState = True
                End Try
        End Select

        If errorState Then
        Else
            'display answer results and add to total/correct answers
            If correctAnswer = CInt(StudentAnswerTextBox.Text) Then
                MsgBox($"Congratulations {NameTextBox.Text} you got the answer correct!")
                correctAnswers += 1
            Else
                MsgBox($"Correct Answer is {correctAnswer}")
            End If
            totalAnswers += 1
        End If

    End Sub
    Private Sub SummaryButton_Click(sender As Object, e As EventArgs) Handles SummaryButton.Click
        'display total correct answers out of total answers
        MsgBox($"Student has gotten {correctAnswers} out of {totalAnswers} answers correct")
    End Sub
    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        'reset form to input a different student
        totalAnswers = 0
        correctAnswers = 0
        NameTextBox.Text = ""
        AgeTextBox.Text = ""
        GradeTextBox.Text = ""
        FirstNumberTextBox.Text = ""
        SecondNumberTextBox.Text = ""
        StudentAnswerTextBox.Text = ""
        AddRadioButton.Checked = True

    End Sub
End Class
