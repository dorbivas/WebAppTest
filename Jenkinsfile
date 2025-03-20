pipeline {
  agent any
  stages {
    stage('Restore') {
      steps {
        sh 'sh \'dotnet restore\''
      }
    }

    stage('Build') {
      steps {
        sh '''sh \'dotnet build --no-restore\'
archiveArtifacts artifacts: \'**/bin/**/*\', fingerprint: true'''
      }
    }

    stage('Test') {
      steps {
        sh '''sh \'dotnet test UnitestWeb/UnitestWeb.csproj --no-build\'
archiveArtifacts artifacts: \'**/TestResults/*.trx\', fingerprint: true'''
      }
    }

  }
}