
#Get started with BAZOOOKA BALL DEVELOPMENT!

# GIT IT!

In this project we use git to collaborate.
We will start of with a small introduction on one way to use git to use git commands to handle 
branches and such.

## Get the repository and dive in to master [OPTIONAL]
To simplify the usage of GIT with github one can create an SSH-key and register it with github.
Use the [GITHUB-guide](https://help.github.com/articles/connecting-to-github-with-ssh/) to setup ssh-keys
and register them to your account.

In your home-catalogue, .gitconfig  you can setup your name and email as below.

    # Required stuff
    [user]
        name = Developery McDeveloperface
        email = developery.vdeveloperface@telavox.se
    [core]
    # Global excludes-fil. Användbar för vanliga tempfiländelser
        excludesfile = ~/.gitignore
    
    # Assert git do not touch line endings
        whitespace = cr-at-eol
        autocrlf = false
    
    [branch]
    # Pulling a branch should execute a _rebase_, not merge.
        autosetuprebase = always
    [merge]
    # Ask for a merge commit message when merging
        log = true
    [rebase]
        # ...om man inte alltid vill specificera `--autosquash` till
        # `git rebase`.
        autosquash = true
    [rerere]
        # Enable remembering conflict resolution. See http://goo.gl/hdAsi3
        enabled = true
       
    # Optional GIT candy
    [alias]
    # Beautiful logs
        lol = log --graph --decorate --pretty=oneline --abbrev-commit
        lola = log --graph --decorate --pretty=oneline --abbrev-commit --all
        lg = log --color --graph --pretty=format:'%Cred%h%Creset -%C(yellow)%d%Creset %s %Cgreen(%cr) %C(bold blue)<%an>%Creset' --abbrev-commit --

    # http://bit.ly/vZMVTL
        fixup = !sh -c 'git commit -m \"fixup! $(git log -1 --format='\\''%s'\\'' $@)\"' -
        squash = !sh -c 'git commit -m \"squash! $(git log -1 --format='\\''%s'\\'' $@)\"' -
        ri = rebase --interactive --autosquash
    [color]
        ui = auto
        diff = auto
        status = auto
        branch = auto
    [color "branch"]
        current = yellow reverse
        local = yellow
        remote = green
    [color "diff"]
        meta = yellow bold
        frag = magenta bold
        old = red bold
        new = green bold
    [color "status"]
        added = yellow
        changed = green
        untracked = cyan

### Cloning REPOSITORY
Position your self in the catalogue where you want the code for instance ***/usr/home/mongobongodongo/git***

    git clone git@github.com:TomasRegnmyr/bazookabongo.git

Done now you have your delightful repository.

## Git branching
This is how manage branching

### Master
This branch is the Productional branch. Everything here should be runnable and completed.

### Creating new branches
It is nice to create branches with names that describe the feature that is being developed 
or that makes the mapping to the ticketing system easy. For instance if i would work on 
something like increasing the size of the balls i would suggest naming it to something like:

    ticketid_grande_cohones

Create the branch by writing ***git checkout -b ticketid_grande_cohones***

### Keep the branch updated
Too prevent large unhandable merge conflicts on ongoing larger sidequests make sure 
you fetch and rebase master frequently. One way of doing so is to fetch and rebase master frequently.

Make sure you stash or commit your changes prior to switching branches.

***git checkout master*** - You are now on master

***git fetch*** - Fetch latest from remote end

***git rebase origin/master*** - Update master branch

Above two steps can be replaced with pull, but nevermind.
Look at the log with

***git log*** - To see changes

Checkout the branch your working on once again

***git checkout branch_im_working_on***

Move it forward so that the same changes that was on master is now on your branch

***git rebase origin/master***

Solve merge conflicts that are indicated in files with >>> and === 

***git add files***

***git commit***

***git push origin branch_im_working_on***



## Additional stuff
***git cherry-pick commithash*** used to add a single commit from other branch.
The hash is seen in github for all comits or in ***git log***

***git revert commithash*** creates a commit which singles out the specified commit

***git checkout branchname -- path/to/file*** used to retrieve a file from another branch in to the working branch

***git stash*** put your current changes in "local-cache" without commiting

***git stash pop* throw in the changes from "local-cache"**



