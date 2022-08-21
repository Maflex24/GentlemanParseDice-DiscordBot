
# Gentleman Parser

Gentleman Dice Parser is a discord bot to roll virtual dice(s), sum your roll, and optional bonuses, show result, and statistics of your roll. 

Antoher functionality is replying for custom command with random reply, avalaible to this command. More details about it below. 


## Roll command

### Syntax

Syntax for rolling is simple: `!5d10-2+5`, where:  
- `!` is command prefix. You can change this prefix to other char, at `Classses/DevelopmentInfo.cs`

```c#
public static char CommandPrefix { get; } = '!';
```

- `5` is how many rolls you want to do. Here is 5 rolls. You can skip this parameter, then you will roll one time
Bot has limit to roll maximum 30 times in one command, for performance security reason
- `d` or `k` is separator, you need to use one of them to run command correctly
- `10` is your dice maximum value, it's not fixed-defined, you can use some strange value, like `3` or `1000` if you want
- `-2` and `+5` are's yours penalties and bonuses. It's summming together, so at this case is `+3`. If your character has some bonuses, and penalties, you don't need to calculate it, bot will do it for you

### Replies

General reply looks like:

```
@YourDiscordNick : TotalRollSum
command | [all single roll values] | [totalBonus]
Average: average value
Power: % of max possibile to roll value
```

Example reply for command `5d6+4`:

```
@YourDiscordNick: 24
5d6+4 | [3, 6, 5, 1, 5] [4]
Average: 4
Power: 66%
```

And few more examples directly from Discord:

![Roll examples](rollExamples.png?raw=true)  

As you can see, there is not information about `average`, and `power`, when roll is simple enough



## Custom Replies

You can edit `multicommands.json` file, to add your commands, and possible replies for them

For exampe:

```json
{
  "hello": [
    "Hey you! How are you?",
    "Wanna diceing today?",
    "If I'm slow today, sorry, I was diceing all night"
  ],
  "mycommand" : [
      "My first reply",
      "My second reply"
  ]
}
```

Then by typing `!mycommand` you will get one of your defined reply (random).
- Be careful of valid .json format, it's easy to make mistake with some unexpected character. You can use tools like [JSON Formatter & Validator](https://jsonformatter.curiousconcept.com/) to check is your file correct
- Bot read file on start, if you edit file when bot works, changes will be avalaible after restart

### Image replies

You can add image to `images` directory, like `images/myimage.png`, and use image as reply. Add in `multicommands.json` image name with extension:

```json
```json
{
  "hello": [
    "Hey you! How are you?",
    "Wanna diceing today?",
    "If I'm slow today, sorry, I was diceing all night"
  ],
  "mycommand" : [
      "My first reply",
      "My second reply",
      "myimage.png"
  ]
}
```

Program will send that image as reply, if this answer will be rolled. Your file need to be in `image` directory, and need to be in one of this format:
- .jpg
- .jpeg
- .gif
- .png 

Don't use too big image. It can slow down reply time too much.
