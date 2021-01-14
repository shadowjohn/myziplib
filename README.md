# myziplib
用 C# 寫的 zip lib ，列檔案、讀資料

Author：
  羽山秋人 ( https://3wa.tw )

Create Date：
  2021-01-14

Usage：

  using System.IO.Compression;<br>
  using System.IO.Compression.FileSystem;<br>
  using myziplib;<br>
  <br>
  //使用二個function：<br> 
  <br>
  myziplib.myziplib myzip = new myziplib.myziplib(); //物件初始化<br>
  <br>
  myzip.getZipFileLists("test.zip"); //取得test.zip 裡面的檔案列表<br>
  myzip.getZipFileLists("test.zip","test/folder1/images.jpg"); //取得壓縮檔裡的資料，回應 byte[]<br>

