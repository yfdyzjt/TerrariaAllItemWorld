const Point typeof System.Drawing.Point

local version = "v2.1.0"

local maxId = 6145

local startChestPos = Point(4741, 1315)
local startFramePos = Point(4741, 1289)

local partStep = Point(45, -45)
local partCount = Point(8, 9)

local worldName = "all_item_world"
local world = LoadWorld(worldName)
print("Load world: " .. world.Name)

local frameId : int = 1
local chestId : int = 1

print("Item frame items:")
local minFrameX : int = startFramePos.X
local maxFrameX : int = startFramePos.X + partStep.X * (partCount.X - 1)
local stepFrameX : int = partStep.X
local minFrameY : int = startFramePos.Y
local maxFrameY : int = startFramePos.Y + partStep.Y * (partCount.Y - 1)
local stepFrameY : int = partStep.Y

for partY : int = minFrameY, maxFrameY, stepFrameY  do
    for partX : int = minFrameX, maxFrameX, stepFrameX do
        if frameId > maxId then break end

        local startId = frameId

        for offsetY : int = 0, 18, 2 do
            for offsetX : int = 0, 18, 2 do
                if frameId > maxId then break end

                local x : int = partX + offsetX
                local y : int = partY + offsetY

                local pos = Point(x, y)

                local frame = Tool.GetTileEntity(world, pos)

                frame.Item.Type = frameId
                frame.Item.StackSize = 1

                frameId = frameId + 1
            end
        end

        print(startId .. " ~ " .. frameId - 1)
    end
end

print("Chest items:")
local minChestX : int = startChestPos.X
local maxChestX : int = startChestPos.X + partStep.X * (partCount.X - 1)
local stepChestX : int = partStep.X
local minChestY : int = startChestPos.Y
local maxChestY : int = startChestPos.Y + partStep.Y * (partCount.Y - 1)
local stepChestY : int = partStep.Y

for partY : int = minChestY, maxChestY, stepChestY  do
    for partX : int = minChestX, maxChestX, stepChestX do
        if chestId > maxId then break end

        local startId = chestId

        for offsetY : int = 0, 6, 3 do
            chestId = startId

            for offsetX : int = 0, 4, 2 do
                if chestId > maxId then break end

                local x : int = partX + offsetX
                local y : int = partY + offsetY

                local pos = Point(x, y)
                local chest = Tool.GetChest(world, pos)

                local beginId = chestId

                for i = 0, ChestProperty.MaxItems - 1, 1 do
                    if chestId > maxId or chestId - startId >= 100 then break end
                        
                    chest.Item[i].StackSize = 9999
                    chest.Item[i].Type = chestId

                    chestId = chestId + 1
                end

                chest.Name = beginId .. " ~ " .. chestId - 1
            end
        end

        print(startId .. " ~ " .. chestId - 1)
    end
end

world.GameMode = 3
world.Name = "橙子的全物品 All Item World " .. version
print("Save world: " .. world.Name)
SaveWorld(world, worldName)

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
