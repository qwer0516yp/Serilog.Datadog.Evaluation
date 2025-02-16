$udpClient = New-Object System.Net.Sockets.UdpClient(7071)
$remoteEndPoint = New-Object System.Net.IPEndPoint ([System.Net.IPAddress]::Any, 0)
while ($true) {
    $receivedBytes = $udpClient.Receive([ref]$remoteEndPoint)
    $message = [System.Text.Encoding]::UTF8.GetString($receivedBytes)
    Write-Host "Received: $message from $($remoteEndPoint.Address):$($remoteEndPoint.Port)"
}