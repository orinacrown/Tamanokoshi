# Tamanokoshi

コードのみ掲載です。突貫作業なのでコメントとか全然なかったりするし、もっと効率のいい記述もあると思いますが参考程度に。

どのcsファイルがどこで使われているのかざっくり記載しておきます。
### BootManaeger
起動後の前書き的な画面で使用しています。どちらかといえばNCMB等の準備がしたいがために前書きを書いています。

### GameManager
メインとなる部分です。リザルトまで管理しています。ゲーム中は主に球の個数をカウントすることと、オレンジの円を展開するかどうかの判定くらいしかしていません。

Startのところでなんか2行ほどコメントアウトされてる残骸がありますが、ゲーム開始時に球を動かすかどうかのもので、結局動かさない方向で決定したのでコメントアウトされています。

### PlayerCircleManager


