#include <stdio.h>
#include <stdlib.h>
#include "NativeLibrary.h"

// 还是需要引入Runtime.ServerGC.lib,将Runtime.ServerGC.lib放到项目的根目录
// #pragma comment(lib, "Runtime.ServerGC.lib")
// #pragma comment(lib, "NativeLibrary.lib")

// 在连接器输入bcrypt.lib这个系统静态库,不然编译不会通过

int main(int arg, char *argv[])
{
    int a = 100;
    int b = 50;
    printf("a=%d  b=%d\n", a, b);
    printf("sum=%d\n", add(a, b));

    char *first = "hello";
    printf("result=%s\n", write_line(first) > -1 ? "ok" : "error");

    return 0;
}