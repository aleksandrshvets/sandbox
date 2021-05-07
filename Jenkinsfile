pipeline {
agent none
  stages {
    stage('Run dotnet-sdk') {
      steps {
        agent {
          kubernetes { 
            yamlFile './KubernetesPod.yaml' 
            label 'some-label-value' 
            } 
        }
        container('dotnet-sdk') {
          sh 'cat'
        }
      }
    }
  }
}