pipeline {
    agent any

    // Good place to set environment variables or credentials
    environment {
        // e.g. if you have a secret in Jenkins Credentials
        // DB_PASSWORD = credentials('DB_PASSWORD')
    }

    stages {
        stage('Checkout') {
            steps {
                // If using GitHub or similar
                checkout scm
            }
        }

        stage('Restore NuGet Packages') {
            steps {
                // On a Linux agent, use 'sh'; on Windows, use 'bat'
                sh "dotnet restore"
            }
        }

        stage('Build Solution') {
            steps {
                sh "dotnet build --no-restore --configuration Release"
                
                // Optionally archive or stash the build outputs
                archiveArtifacts artifacts: '**/bin/Release/**/*', fingerprint: true
            }
        }

        stage('Run Unit Tests') {
            steps {
                sh "dotnet test --no-build --configuration Release --logger 'trx;LogFileName=test_results.trx'"
            }
            post {
                always {
                    // Publish test results so Jenkins tracks pass/fail stats
                    junit '**/test_results.trx'
                }
            }
        }

        stage('Publish Artifacts') {
            steps {
                // Publish for a self-contained deployment if needed
                sh "dotnet publish --no-build --configuration Release -o out"

                // Archive the published output (or stash) for later deploy steps
                archiveArtifacts artifacts: 'out/**/*', fingerprint: true
            }
        }

        stage('Deploy to Test Environments') {
            parallel {
                stage('Deploy to QA-Env-A') {
                    steps {
                        sh """
                            echo "Deploying to QA-Env-A..."
                            # e.g. copy files, run scripts, or call an installer
                            # scp ./out/* user@qa-env-a:/deployments/...
                            # or use a tool like PowerShell Remoting if on Windows
                        """
                    }
                }
                stage('Deploy to QA-Env-B') {
                    steps {
                        sh """
                            echo "Deploying to QA-Env-B..."
                            # Similar approach for QA-Env-B
                        """
                    }
                }
            }
        }

        stage('Approval for Production') {
            when {
                // Only run if the QA steps succeed, 
                // or you can add your own conditions
                expression { return currentBuild.resultIsBetterOrEqualTo('SUCCESS') }
            }
            steps {
                // This step will cause Jenkins to pause and wait for a user action
                input message: "Proceed with deployment to Production?"
            }
        }

        stage('Deploy to Production') {
            steps {
                sh """
                    echo "Deploying to Production Environment(s)..."
                    # Similar deployment steps as QA, 
                    # but referencing production servers
                """
            }
        }
    }

    post {
        always {
            // Clean up, notify, or do any final tasks
            echo "Pipeline finished. Cleaning workspace..."
            cleanWs()
        }
    }
}
