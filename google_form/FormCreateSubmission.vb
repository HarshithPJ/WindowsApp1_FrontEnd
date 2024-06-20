Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Text
Imports System.Diagnostics

Public Class FormCreateSubmission
    Private stopwatch As New Stopwatch()

    Private Sub btnStopwatch_Click(sender As Object, e As EventArgs) Handles btnStopwatch.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            btnStopwatch.Text = "Start"
        Else
            stopwatch.Start()
            btnStopwatch.Text = "Stop"
        End If
        lblStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim submission = New With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhoneNumber.Text,
            .github_link = txtGitHubRepo.Text,
            .stopwatch_time = lblStopwatch.Text
        }

        Dim json = JsonConvert.SerializeObject(submission)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")

        Using client As New HttpClient()
            Dim response = Await client.PostAsync("http://localhost:3000/submit", content)
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission successful!")
            Else
                MessageBox.Show("Submission failed.")
            End If
        End Using
    End Sub

    Private Sub FormCreateSubmission_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnStopwatch.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub

    Private Sub lblStopwatch_Click(sender As Object, e As EventArgs) Handles lblStopwatch.Click

    End Sub
End Class
