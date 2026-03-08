dotnet tool install -g dotnet-reportgenerator-globaltool
$TestOutput = dotnet test --collect "XPlat Code Coverage" --results-directory ./BuildReports/UnitTests
$TestReportsParts = $TestOutput | Select-String coverage.cobertura.xml | ForEach-Object { $_.Line.Trim() }
$TestReportsCrappy = ($TestReportsParts -join ';')

$guid_regex = "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}" 
$TestReports = $TestReportsCrappy -replace $guid_regex
$TestReports = $TestReports.Replace("//","/").Replace('\\','\') #.Replace("\\UnitTests","\\Coverage")

copy $TestReportsCrappy $TestReports
del $TestReportsCrappy

Get-ChildItem -Path ./BuildReports/UnitTests -Directory -Recurse | Remove-Item -Force  

reportgenerator "-reports:$TestReports" "-targetdir:.//BuildReports//Coverage" "-reporttype:Html" "-classfilters:-AspNetCoreGeneratedDocument.*"

start "BuildReports\Coverage\index.htm"
# SIG # Begin signature block
# MIIFTAYJKoZIhvcNAQcCoIIFPTCCBTkCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUjRbugHnnO1pQ5ZZWXqBD/1uH
# GV6gggLyMIIC7jCCAdagAwIBAgIQHOu0aZHZxIFEVwbKIEdWITANBgkqhkiG9w0B
# AQsFADAPMQ0wCwYDVQQDDARNYXJjMB4XDTI2MDIyODA1MDI1MloXDTI3MDIyODA1
# MjI1MlowDzENMAsGA1UEAwwETWFyYzCCASIwDQYJKoZIhvcNAQEBBQADggEPADCC
# AQoCggEBAMBtwp/RrYhrMKWC5Qjf5hQHAO+SgAG8WgvxmNvzwlDj2qV9bGiLqEtj
# EtGV9GWPSRW8zxf5j9+HuvPV4gR5TglWGg+lNJb9t8V69mVqOryS9Rch4V+dK+Lb
# ZOxGVAIrk2kUGDVof4Oa1rlQLbFtw2suKhDP4y2pEm5UReDkh/J1tHhbEbgtyAOn
# zRkMx/5r5x3x45Bgw5av7hP1W9djS9Qj3rbPUM3EXuzNrAI0HIbylmSyVQ1r42LU
# AYd62NglTH/+0+qOv0EBwTtraG45f3BvnGnCnf2ImSB3lMp5PnNf/y92Ju1IBphY
# 1WOYkD/4A/cnZV7UctpWe2zNzHNIdZUCAwEAAaNGMEQwDgYDVR0PAQH/BAQDAgeA
# MBMGA1UdJQQMMAoGCCsGAQUFBwMDMB0GA1UdDgQWBBQgOlHw44MqfvrVu6txZzGz
# rd586zANBgkqhkiG9w0BAQsFAAOCAQEAeUYWDDz3HNEU1NvB73VEr+BNWwwr2Fii
# EshFjpUxvW/ztBafyhyRLyvFUaeYW8e312PUGuyY/r8eO04FpbnizbtL7pjTSsS9
# R8K40iuoEi/qoZW+ibhz1TfLqA+WK3xgqwyNmgUc4fMXFmE4SPt/9s2jIX3hnuVG
# Xq83EGEpdTUC3S0HYohiq7dZpiz10fH5+rIU5SmicbHUpkVTfr9IovMJHgWfWqA9
# d81W8mE2oY+hCYunAhyQI5qXAj+90Fryt5OSK4cjSVCkiTuzQQK5s0C6NsWzxW2S
# KmTfk/XaW6z6Zn0rAP1fJnC0YmEQOLUyEB/7LtZ9V4wVEHzBcH0EJjGCAcQwggHA
# AgEBMCMwDzENMAsGA1UEAwwETWFyYwIQHOu0aZHZxIFEVwbKIEdWITAJBgUrDgMC
# GgUAoHgwGAYKKwYBBAGCNwIBDDEKMAigAoAAoQKAADAZBgkqhkiG9w0BCQMxDAYK
# KwYBBAGCNwIBBDAcBgorBgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG
# 9w0BCQQxFgQUYyBwa2uKnJMfFyKstbA/aA5Q7r4wDQYJKoZIhvcNAQEBBQAEggEA
# hk4RgofNJl0MAuZNnjpQRAWDx5sQT/1azKnhr5LJMzSDqCCX0lYQJhF8b0gf78mA
# lSA39mkU+2J/1l/HLoUWPo/sboEnkb/Ffvy3haefSr8Q9deEoxsKsEvMUn9O7ZVm
# wiKQNjvbIlpdpFMxh+Br4WlnV+4a+jdK9GRsYZDgQ1vh4keKktdFrmpPv/MvoBKQ
# XqknsMWPvqbE1iLRsegsgncqLvCF2de2H5D1JiwVFjbdiVPUDXEoJksqmOjZZYYx
# A5yo+MiK9aN0t46sv2lLnMq+DlhzZroYCBxHzNwaHpz22HdGtNzc/R85KaN48OM9
# yS7cw8KqeGgAmBFfyzrnoQ==
# SIG # End signature block
