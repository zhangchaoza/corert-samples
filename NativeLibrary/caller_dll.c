#include <stdio.h>
#include <stdlib.h>
#include <windows.h>

int main(int arg, char *argv[])
{
    int a = 100;
    int b = 50;
    printf("a=%d  b=%d\n", a, b);

    HMODULE handle = LoadLibraryA("NativeLibrary.dll");
    if (handle != NULL)
    {
        // 为add声明函数指针Add
        typedef int (*Add)(int, int);

        // 为write_line声明函数指针Write_line
        typedef int (*Write_Line)(char *str);

        // 为sumstring声明函数指针Sumstring
        typedef char *(*SumString)(char *first, char *second);

        Add sum = (Add)GetProcAddress(handle, "add");
        printf("sum=%d\n", sum(a, b));

        char *first = "hello";
        Write_Line write_line = (Write_Line)GetProcAddress(handle, "write_line");
        printf("result=%s\n", write_line(first) > -1 ? "ok" : "error");

        char second[] = " world";

        SumString sumstring = (SumString)GetProcAddress(handle, "sumstring");
        printf("sumstring=%s\n", sumstring(first, second));

        // 句柄使用结束,要记得释放,避免句柄泄露
        CloseHandle(handle);
    }

    return 0;
}