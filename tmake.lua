local version = "v2.1.0"

local worldName = "all_item_world"
local world = LoadWorld("world/" .. worldName)
print("Load world: " .. world.Name)

print("Start id:")
Include(self, "id/id").Id(world)
print("Start class:")
Include(self, "class/class").Class(world)
print("Start bestiary:")
Include(self, "bestiary/bestiary").Bestiary(world)

world.GameMode = 3
world.Name = "橙子的全物品 🍊 All Item World " .. version
print("Save world: " .. world.Name)
SaveWorld(world, "final/" .. worldName)

-- world.GameMode = 0
-- world.Name = "橙子的全物品 All Item World (classic)"
-- print("Save world: " .. world.Name)
-- SaveWorld(world, worldName .. "_classic")

-- world.GameMode = 1
-- world.Name = "橙子的全物品 All Item World (expert)"
-- print("Save world: " .. world.Name)
-- SaveWorld(world, worldName .. "_expert")

-- world.GameMode = 2
-- world.Name = "橙子的全物品 All Item World (master)"
-- print("Save world: " .. world.Name)
-- SaveWorld(world, worldName .. "_master")
