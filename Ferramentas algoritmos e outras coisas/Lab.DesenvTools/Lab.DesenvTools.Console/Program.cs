using Lab.DesenvTools.Console;

var trie = new TrieNode();

List<List<string>> queries =
[
    ["add", "hack"],
    ["add", "hackerrank"],
    ["find", "hac"],
    ["find", "hak"],
];

var result = new List<int>();

queries.ForEach(q => {
    var command = q[0];
    var item = q[1];

    if (command == "add")
    {
        trie.Insert(item);
    }
    else if (command == "find")
    {
        var node = trie.GetNode(item);

        if (node is null)
        {
            result.Add(0);
            return;
        }

        result.Add(node.Count);
    }
});

Console.WriteLine($"[{string.Join(", ", result)}]");
Console.ReadLine();