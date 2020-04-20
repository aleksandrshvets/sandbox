node {
def currentDirectory = pwd()
checkout scm
def scriptPath = "D:\\GitHub\\sandbox\\ci\\jenkins\\secondfolder\\secondscript.ps1 -path D:\\GitHub\\sandbox\\ci\\jenkins\\firstfolder\\firstscript.ps1 -parameter b"
echo "${scriptPath}"



def mainRes = powershell returnStdout: true, script: "${scriptPath}"
echo "${mainRes}"
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