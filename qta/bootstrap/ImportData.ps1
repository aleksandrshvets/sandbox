function Get-SuggestorPhrases(
    [PSCustomObject]$item
) {
    $pharses = @()
    #$phrase = $item.phrase -replace "[^a-zA-Z0-9_\s]", ""
    
    $words = $item.phrase.Split(" ", [System.StringSplitOptions]::RemoveEmptyEntries )
    while ($words.Length -ne 1) {
        $pharses += "$words"
        $words = $words[1..($words.Length-1)]
    }
    return $pharses
}

function Get-SuggestorPhrasesHyphenSplit(
    [PSCustomObject]$item
) {
    $pharses = @()
    #$phrase = $item.phrase -replace "[^a-zA-Z0-9_\s]", ""

    $hyphenWords = $item.phrase.Split("-", [System.StringSplitOptions]::RemoveEmptyEntries )
    while ($hyphenWords.Length -ne 1) {
        $words = $hyphenWords.Split(" ", [System.StringSplitOptions]::RemoveEmptyEntries )
        while ($words.Length -ne 1) {
            $pharses += "$words"
            $words = $words[1..($words.Length-1)]
        }
        $hyphenWords = $hyphenWords[1..($hyphenWords.Length-1)]
    }

    return $pharses
}


function Get-SuggestorInputWords(
    [PSCustomObject]$item
) {
    return $item.phrase
    $phrase = $item.phrase -replace "[^a-zA-Z0-9_\s\-]", ""
    return $phrase
    $words = $phrase.Split(" ", [System.StringSplitOptions]::RemoveEmptyEntries )
    return $words
}


$ScriptHome = $PSScriptRoot
$dataString = Get-Content -Path $ScriptHome/data.json -Raw
$dataObj = $dataString | ConvertFrom-Json
$data = $dataObj.data
$errorList = @();
foreach ($item in $data) {
    $words = Get-SuggestorPhrases $item
    $phraseSuggest = @{
        input  = $words
        weight = $item.pageRank
    }

    $itemToIndex = @{
        pageRank      = $item.pageRank
        pages         = $item.pages
        phrase        = $item.phrase
        phraseSuggest = $phraseSuggest
    }


    $itemJson = $itemToIndex | ConvertTo-Json
    
    try {
        #Invoke-RestMethod -Uri http://127.0.1:9200/qta-poc/doc/ -Method POST -Body $itemJson -ContentType 'application/json'
        Invoke-RestMethod -Uri http://10.245.1.21/elastic/qta-poc/doc/ -Method POST -Body $itemJson -ContentType 'application/json'

    }
    catch {
        $errorList += $itemJson
    }
}
$errorJson = $errorList | ConvertTo-Json
Write-Host $errorJson