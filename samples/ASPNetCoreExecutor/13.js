﻿#!/usr/bin/env node
console.log("xxl-job: hello nodejs")

var arguments = process.argv

console.log("脚本位置: " + arguments[1])
console.log("任务参数: " + arguments[2])
console.log("分片序号: " + arguments[3])
console.log("分片总数: " + arguments[4])

console.log("Good bye!")
process.exit(0)
