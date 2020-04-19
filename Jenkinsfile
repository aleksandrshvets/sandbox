node {
def currentDirectory = pwd()

def scriptPath = "${currentDirectory}\\sandbox\\ci\\jenkins\\secondfolder\\secondscript.ps1"
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