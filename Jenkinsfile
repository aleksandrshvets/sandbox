pipeline {
   agent any

   stages {
      stage('Hello') {
         steps {
		def pipeline
        pipeline = load 'DoJob.groovy'
        pipeline.firstTest()
         }
      }
   }
}