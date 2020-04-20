node {
def currentDirectory = pwd()
checkout scm
def res= powershell returnStdout: true, script: "Get-ChildItem -Path D:\\Jenkins\\workspace\\first"
echo "${res}"
echo "${WORKSPACE}"
def scriptPath = "${currentDirectory}\\ci\\jenkins\\secondfolder\\secondscript.ps1"
echo "${scriptPath}"

powershell returnStdout: true, script: "${scriptPath}"
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