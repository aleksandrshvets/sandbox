def firstTest() {
}

def testTwo() {
}
testTwo()
return [
    firstTest: this.&firstTest,
    testTwo: this.&testTwo
]