podTemplate(yaml: """
metadata:
  labels:
    some-label: some-label-value
spec:
  containers:
  - name: dotnet-sdk
    image: mcr.microsoft.com/dotnet/sdk:3.1
    command:
    - cat
    tty: true
"""
  ) {

  node(POD_LABEL) {
    stage('Run dotnet-sdk') {
      git 'https://github.com/aleksandrshvets/sandbox.git'
    }
  }
}