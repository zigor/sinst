pipeline {
  agent any
  stages {
    stage('checkout sprint') {
      parallel {
        stage('checkout sprint') {
          steps {
            powershell 'checkout sprint'
          }
        }
        stage('checkout release') {
          steps {
            powershell 'werwer'
          }
        }
      }
    }
    stage('build sprint') {
      parallel {
        stage('build sprint') {
          steps {
            powershell 'build sprint'
          }
        }
        stage('build release') {
          steps {
            powershell 'sdf'
          }
        }
      }
    }
  }
}