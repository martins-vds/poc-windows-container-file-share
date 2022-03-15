[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [ValidateNotNull()]
    [System.Uri]
    $Url,
    [Parameter(Mandatory = $true)]
    [int]
    [ValidateRange(100, 500)]
    $ExpectedStatusCode
)

try {
    Write-Output "Sending HTTP GET request to '$($Url.AbsoluteUri)'..."
    $response = Invoke-WebRequest -Uri $Url -TimeoutSec 30 -MaximumRetryCount 3 -RetryIntervalSec 5
    if ($response.StatusCode -eq $ExpectedStatusCode){
        Write-Output "Smoke test passed."
    }else{
        Write-Output "::error title=Smoke test failed::Expected status code '$ExpectedStatusCode' but got '$($response.StatusCode)' instead"
    }
}
catch {
    Write-Output "::error title=Smoke test failed::$($_.Exception.Message)"
}