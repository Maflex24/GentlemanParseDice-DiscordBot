# GentlemanParseDice-DiscordBot

## Features
### Dice Parser
Syntax looks like:
`![howmanyrolls]d[whatdice]+bonus-penalty`

So for example:
`!5d6+4` rolls for you 5 times dice 6 and finally add +4 of bonus. 
Against `d` you can use `k` if you prefer.
If you not write any number before 'd' or 'k' it rolls one time. So example corrects syntax are: `1d4`, `2d10-2`, `5k6`, `30k16`. Don't try rolls more than 30 times in one command, Gentleman don't like it.

You have information about every roll, average and percent of maximum possibile roll. If you for example roll `!5d6+4` for damage and you get together 17, you will get information that's 43% of possible power. So.. I'm sorry for you ;)    
[5d6plus4.png]  
Like you see on image bot also tag you, so no longer problem if two people rolls at the same time, and you don't know which rolls is for who.   

### Custom replies
Some files are not included in github, like bot token.txt, path.txt, and multicommands.json. Token is my private key to bot, path.txt keep path to multicommands.json file, where I keep custom commands. It looks like: 
```json
{
    "hello": [
        "Hello, How are you?",
        "Nah, I'm not in mood today. Hope you are better!",
        "Roll me!"
    ]
}
```
So, at this structure by type `!hello` you will get one of the possible answers. 
