(for %%I in (.) do set CurrDirName=%%~nxI)
copy "D:\Programs\asp.net core .gitignore\.gitignore" .gitignore
git init && git add --all && git commit -m "first commit" && git remote add origin "https://github.com/Adagaki6809/%CurrDirName%.git" && git push origin master


PAUSE