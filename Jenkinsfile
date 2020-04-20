node {
def currentDirectory = pwd()
checkout scm
def scriptPath = "${currentDirectory}\\ci\\jenkins\\second@folder\\secondscript.ps1 -path ${currentDirectory}\\ci\\jenkins\\first@folder\\firstscript.ps1 -parameter b"
echo "${scriptPath}"



def mainRes = powershell returnStdout: true, script: "${scriptPath}"
echo "${mainRes}"

def dirs = powershell returnStdout: true, script: "Get-ChildItem ${currentDirectory}"
echo "${dirs}"

if (mainRes.trim() == "True"){
echo "true!!!!!!!!!!!"
}
else {
echo "false!!!!!!!!!!!"
}

   stage 'test'
   def whatThe = someFunc('textToFunc')
   def whatThe2 = someFunc2('textToFunc2')
}

def someFunc(String text){
    echo "text"

}
def someFunc2(String text2){
    echo "text2"

}