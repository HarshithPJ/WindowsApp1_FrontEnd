Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text

Public Class FormViewSubmissions
    Private currentIndex As Integer = 0
    Private submissions As List(Of Submission) = New List(Of Submission)()

    Private Sub FormViewSubmissions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSubmissions()
    End Sub

    Private Async Sub LoadSubmissions()
        Using client As New HttpClient()
            Dim response = Await client.GetAsync("http://localhost:3000/read?index=0")

            If response.IsSuccessStatusCode Then
                Dim json = Await response.Content.ReadAsStringAsync()
                submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)
                If submissions.Count > 0 Then
                    DisplaySubmission(submissions(currentIndex))
                End If
            Else
                MessageBox.Show("Failed to load submissions.")
            End If
        End Using
    End Sub

    Private Sub DisplaySubmission(submission As Submission)
        lblName.Text = submission.name
        lblEmail.Text = submission.email
        lblPhoneNumber.Text = submission.phone
        lblGitHubRepo.Text = submission.github_link
        lblTimeSpent.Text = submission.stopwatch_time
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(submissions(currentIndex))
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(submissions(currentIndex))
        End If
    End Sub

    Private Sub FormViewSubmissions_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        End If
    End Sub
End Class

Public Class Submission
    Public Property name As String
    Public Property email As String
    Public Property phone As String
    Public Property github_link As String
    Public Property stopwatch_time As String
End Class
