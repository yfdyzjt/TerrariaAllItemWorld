const Point typeof System.Drawing.Point
const Rectangle typeof System.Drawing.Rectangle

Include(self, "categories")

local oldMaxId = 5455

local startPosX = 1416
local startPosY = 1671

local classStep : int = 786
local layerStep : int = 9
local partStep : int = 14
local partCount : int = 12

local leftChestOffset : int = 58
local leftFrameOffset : int = 52
local leftSignOffset : int = 44
local rightChestOffset : int = 53
local rightFrameOffset : int = 51
local rightSignOffset : int = 43

local worldName = "all_item_world"
local world = LoadWorld(worldName)
print("Load world: " .. world.Name)

local signData = FrameProperty.GetFrameData(55, 4)
local frameData = FrameProperty.GetFrameData(395)
local chestData = FrameProperty.GetFrameData(21, 47)

local dir : int = 1

for n, class in ipairs(categories.Categories) do
    print(class.Name)

    local leftLayer : int = 0
    local rightLayer : int = 0

    local originPosX : int = startPosX + (n - 1) * classStep
    local originPosY : int = startPosY

    for _, sub in ipairs(class.Subs) do
        print("    " .. sub.Name)

        local count : int = 1
        local i : int = 0
        local part : int = 0
        local layer : int = dir < 0 and leftLayer or rightLayer
        local firstItem : int = 0

        local chestOffset : int = dir < 0 and leftChestOffset or rightChestOffset
        local frameOffset : int = dir < 0 and leftFrameOffset or rightFrameOffset
        local signOffset : int = dir < 0 and leftSignOffset or rightSignOffset

        local signPosX : int = originPosX + dir * signOffset
        local signPosY : int = originPosY - layer * layerStep - 4
        local signPos = Point(signPosX, signPosY)
        local sign = Tool.PlaceSign(world, signPos, signData)
        sign.Text = sub.Name

        for _, id in ipairs(sub.Items) do
            local framePosX : int = originPosX + dir * (part * partStep + frameOffset + i % 5 * 2)
            local framePosY : int = originPosY - layer * layerStep - 6 + math.floor(i / 5) * 2
            local framePos = Point(framePosX, framePosY)
            local frame = Tool.PlaceTileEntity(world, framePos, frameData)

            frame.Item.Type = id
            frame.Item.StackSize = 1

            world.Tile[framePos.X, framePos.Y].InvisibleBlock = true
            world.Tile[framePos.X, framePos.Y + 1].InvisibleBlock = true
            world.Tile[framePos.X + 1, framePos.Y].InvisibleBlock = true
            world.Tile[framePos.X + 1, framePos.Y + 1].InvisibleBlock = true

            if id > oldMaxId then
                world.Tile[framePos.X, framePos.Y].WallColor = 13
                world.Tile[framePos.X, framePos.Y + 1].WallColor = 13
                world.Tile[framePos.X + 1, framePos.Y].WallColor = 13
                world.Tile[framePos.X + 1, framePos.Y + 1].WallColor = 13
            end

            for c : int = 0, 1, 1 do
                local chestPosX : int = originPosX + dir * (part * partStep + chestOffset) + c * 4
                local chestPosY : int = originPosY - layer * layerStep - 1
                local chestPos = Point(chestPosX, chestPosY)

                if firstItem < 2 then 
                    Tool.PlaceChest(world, chestPos, chestData) 
                    firstItem = firstItem + 1
                end

                local chest = Tool.GetChest(world, chestPos);

                chest.Item[i].StackSize = 9999
                chest.Item[i + 10].StackSize = 9999
                chest.Item[i + 20].StackSize = 9999
                chest.Item[i + 30].StackSize = 9999
                chest.Item[i].Type = id
                chest.Item[i + 10].Type = id
                chest.Item[i + 20].Type = id
                chest.Item[i + 30].Type = id

                chest.Name = sub.Name .. " " .. count
            end

            i = i + 1
            if i >= 10 then
                i = 0
                part = part + 1
                count = count + 1
                firstItem = 0
                if part >= partCount then
                    part = 0
                    layer = layer + 1
                end
            end
        end

        if dir < 0 then
            leftLayer = layer + 1
        else
            rightLayer = layer + 1
        end

        if leftLayer < rightLayer then
            dir = -1
        else
            dir = 1
        end
    end
end

world.GameMode = 3
world.Name = "橙子的全物品 All Item World (journey)"
print("Save world: " .. world.Name)
SaveWorld(world, worldName)

world.GameMode = 0
world.Name = "橙子的全物品 All Item World (classic)"
print("Save world: " .. world.Name)
SaveWorld(world, worldName .. "_classic")

world.GameMode = 1
world.Name = "橙子的全物品 All Item World (expert)"
print("Save world: " .. world.Name)
SaveWorld(world, worldName .. "_expert")

world.GameMode = 2
world.Name = "橙子的全物品 All Item World (master)"
print("Save world: " .. world.Name)
SaveWorld(world, worldName .. "_master")
