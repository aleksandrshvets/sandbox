def firstTest() {
}

def testTwo() {

}

return [
    firstTest: this.&firstTest,
    testTwo: this.&testTwo
]