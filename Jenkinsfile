pipeline {
  agent any
  stages {
    stage('Restore') {
      steps {
        sh 'dotnet restore'
      }
    }

    stage('Build') {
      steps {
        sh '''dotnet build --no-restore
archiveArtifacts artifacts: \'**/bin/**/*\', fingerprint: true'''
      }
    }

    stage('Test') {
      steps {
        sh '''dotnet test UnitestWeb/UnitestWeb.csproj --no-build
archiveArtifacts artifacts: \'**/TestResults/*.trx\', fingerprint: true'''
      }
    }

  }
}