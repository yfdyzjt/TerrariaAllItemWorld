function Bestiary(world)
    Include(self, "bestiary/npcBestiary")

    world.Bestiary.KillCountsByNpcId.Clear()
    world.Bestiary.WasNearPlayer.Clear()
    world.Bestiary.ChattedWithPlayer.Clear()

    for _, name in ipairs(npcBestiary.NpcBestiary) do
        world.Bestiary.KillCountsByNpcId[name] = 999
        world.Bestiary.WasNearPlayer.Add(name)
        world.Bestiary.ChattedWithPlayer.Add(name)
    end
    
    print("final!")
end
