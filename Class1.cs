using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Util;
using Vintagestory;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.API.Datastructures;
using System.Threading;
using Vintagestory.API;
using Vintagestory.ServerMods;

namespace omniummod
{
    public class Omniummod : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            //---------Entities
            api.RegisterEntity("DeepWaterFish", typeof(DeepWaterFish));
            api.RegisterEntity("EntityFish", typeof(EntityFish));
            api.RegisterEntity("Souldrinker", typeof(Souldrinker));
            api.RegisterEntity("Soul", typeof(Soul));
            api.RegisterEntity("Boss", typeof(Boss));
            api.RegisterEntity("Bosssummoner", typeof(Bosssummoner));
            api.RegisterEntity("BossPrOne", typeof(bossprone));
            api.RegisterEntity("BossPrSecond", typeof(bossprsecond));
            api.RegisterEntity("IceblockPr", typeof(IceblockPr));
            api.RegisterEntity("Mastermade", typeof(Mastermade));

            //------------Blocks
            api.RegisterBlockClass("hydrothermal_vent", typeof(hydrothermal_vent));
            api.RegisterBlockClass("Coral", typeof(Coral));
            api.RegisterBlockClass("Sugarkelp", typeof(Sugarkelp));
            api.RegisterBlockClass("Giantkelp", typeof(Giantkelp));
            api.RegisterBlockClass("Bullkelp", typeof(Bullkelp));
            api.RegisterBlockClass("Seagrass", typeof(Seagrass));
            api.RegisterBlockClass("Mushroomrootedstone", typeof(Mushroomrootedstone));

            api.RegisterBlockClass("Fishbucket", typeof(Fishbucket));

            api.RegisterBlockClass("Altar", typeof(Altar));
            api.RegisterBlockClass("Dungeondoor", typeof(Dungeondoor));
            api.RegisterBlockClass("Dungeondoor_PH", typeof(Dungeondoor_PH));
            api.RegisterBlockClass("Firetrap", typeof(Firetrap));
            api.RegisterBlockClass("Spikes", typeof(Spikes));
            api.RegisterBlockClass("Firetrap_PH", typeof(Firetrap_PH));
            api.RegisterBlockClass("Soulgolem", typeof(Soulgolem_Block));
            api.RegisterBlockClass("Iceblock", typeof(Iceblock));

            //------------BlockEntities
            api.RegisterBlockEntityClass("DungeondoorEntity", typeof(DungeondoorEntity));

            //------------Items and dependencies to them
            api.RegisterItemClass("scourgethrower", typeof(Scourgethrower));
            api.RegisterEntity("Scourgethrown", typeof(Scourgethrown));

            api.RegisterItemClass("Scullsparkle", typeof(Scullsparkle));
            api.RegisterEntity("Sparklescull", typeof(Sparklescull));
        }
    }

    //---------------Entities

    public class DeepWaterFish : EntityFish
    {
        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);

            ServerPos.Motion.Y = -0.01;
        }
    }

    public class EntityFish : EntityAgent
    {

        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);

            if (controls.TriesToMove == true)
            {

            }
            else
            {
                this.controls.FlyVector = new Vintagestory.API.MathTools.Vec3d(0, 0, 0);
                this.controls.FlyMode = false;
                this.controls.WalkVector = new Vintagestory.API.MathTools.Vec3d(0, 0, 0);
            }

            if (Swimming == true)
            {
                ServerPos.Motion.Y = -0.005;
                if (ServerPos.Motion.X > 0) ServerPos.Motion.X -= 0.01;
                if (ServerPos.Motion.X < 0) ServerPos.Motion.X += 0.01;
                if (ServerPos.Motion.Z > 0) ServerPos.Motion.Z -= 0.01;
                if (ServerPos.Motion.Z < 0) ServerPos.Motion.Z += 0.01;
            }
        }

        public override void OnInteract(EntityAgent byEntity, ItemSlot slot, Vec3d hitPosition, EnumInteractMode mode)
        {
            base.OnInteract(byEntity, slot, hitPosition, mode);

            if (this.Alive == true)
            {
                if (mode == EnumInteractMode.Interact)
                {
                    try
                    {
                        if (byEntity is EntityPlayer)
                        {
                            ItemStack stack = ((EntityPlayer)byEntity).Player.InventoryManager.ActiveHotbarSlot.Itemstack;

                            //((ICoreClientAPI)(((EntityPlayer)byEntity).Player.Entity.World.Api)).SendChatMessage("Code = " + this.Code);

                            if (stack.Block.Id == byEntity.World.BlockAccessor.GetBlock(new AssetLocation("omniummod:fischbucket-empty")).Id)
                            {

                                ItemSlot targetSlot = ((EntityPlayer)byEntity).Player.InventoryManager.ActiveHotbarSlot;
                                targetSlot = new DummySlot(targetSlot.TakeOut(1));
                                ((EntityPlayer)byEntity).Player.InventoryManager.ActiveHotbarSlot.MarkDirty();

                                if (this.Code.Equals(new AssetLocation("omniummod:blobfisch")) || this.Code.Equals(new AssetLocation("omniummod:blobfischnd")))
                                {
                                    ((EntityPlayer)byEntity).Player.InventoryManager.TryGiveItemstack(new ItemStack(byEntity.World.GetBlock(new AssetLocation("omniummod:fischbucket-blobfisch")), 1), true);
                                    this.Die(EnumDespawnReason.Removed);
                                }
                                if (this.Code.Equals(new AssetLocation("omniummod:geisterfisch")) || this.Code.Equals(new AssetLocation("omniummod:geisterfischnd")))
                                {
                                    ((EntityPlayer)byEntity).Player.InventoryManager.TryGiveItemstack(new ItemStack(byEntity.World.GetBlock(new AssetLocation("omniummod:fischbucket-geisterfisch")), 1), true);
                                    this.Die(EnumDespawnReason.Removed);
                                }
                                if (this.Code.Equals(new AssetLocation("omniummod:hecht")) || this.Code.Equals(new AssetLocation("omniummod:hechtnd")))
                                {
                                    ((EntityPlayer)byEntity).Player.InventoryManager.TryGiveItemstack(new ItemStack(byEntity.World.GetBlock(new AssetLocation("omniummod:fischbucket-hecht")), 1), true);
                                    this.Die(EnumDespawnReason.Removed);
                                }
                                if (this.Code.Equals(new AssetLocation("omniummod:lachs")) || this.Code.Equals(new AssetLocation("omniummod:lachsnd")))
                                {
                                    ((EntityPlayer)byEntity).Player.InventoryManager.TryGiveItemstack(new ItemStack(byEntity.World.GetBlock(new AssetLocation("omniummod:fischbucket-lachs")), 1), true);
                                    this.Die(EnumDespawnReason.Removed);
                                }
                                if (this.Code.Equals(new AssetLocation("omniummod:butterflifish")) || this.Code.Equals(new AssetLocation("omniummod:butterflifishnd")))
                                {
                                    ((EntityPlayer)byEntity).Player.InventoryManager.TryGiveItemstack(new ItemStack(byEntity.World.GetBlock(new AssetLocation("omniummod:fischbucket-butterflifish")), 1), true);
                                    this.Die(EnumDespawnReason.Removed);
                                }
                                if (this.Code.Equals(new AssetLocation("omniummod:höhlenfisch")) || this.Code.Equals(new AssetLocation("omniummod:höhlenfischnd")))
                                {
                                    ((EntityPlayer)byEntity).Player.InventoryManager.TryGiveItemstack(new ItemStack(byEntity.World.GetBlock(new AssetLocation("omniummod:fischbucket-höhlenfisch")), 1), true);
                                    this.Die(EnumDespawnReason.Removed);
                                }


                            }
                            else
                            {
                                ((ICoreClientAPI)((EntityPlayer)byEntity).Player.Entity.Api).ShowChatMessage("You need a empty fishbucket to catch the fish");
                            }
                        }
                    }
                    catch { }
                }
            }
        }
    }

    public class Souldrinker : EntityAgent
    {
        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);
            FeetInLiquid = false;

            if (ServerPos.SquareDistanceTo(Pos.XYZ) > 0.01)
            {
                float desiredYaw = (float)Math.Atan2(ServerPos.X - Pos.X, ServerPos.Z - Pos.Z);

                float yawDist = GameMath.AngleRadDistance(SidedPos.Yaw, desiredYaw);
                Pos.Yaw += GameMath.Clamp(yawDist, -35 * dt, 35 * dt);
                Pos.Yaw = Pos.Yaw % GameMath.TWOPI;
            }
        }
    }

    public class Soul : Souldrinker
    {
        public override void Initialize(EntityProperties properties, ICoreAPI api, long InChunkIndex3d)
        {
            base.Initialize(properties, api, InChunkIndex3d);
            //this.controls.FlyMode = true;
        }
    }

    public class Bosssummoner : Souldrinker
    {
        public bool teleport = true;
        public BlockPos spawnpos = null;

        public override void Die(EnumDespawnReason reason = EnumDespawnReason.Death, DamageSource damageSourceForDeath = null)
        {
            base.Die(reason, damageSourceForDeath);

            EntityProperties type = this.World.GetEntityType(new AssetLocation("omniummod:boss"));
            Entity ent = this.World.ClassRegistry.CreateEntity(type);
            ent.ServerPos.SetPos(this.ServerPos);
            ent.ServerPos.Motion.Set(0, 0, 0);
            ent.Pos.SetFrom(ent.ServerPos);
            ((Boss)ent).teleport = this.teleport;
            ((Boss)ent).spawnpos = this.spawnpos;
            World.SpawnEntity(ent);
        }
    }



    public class Boss : EntityAgent
    {
        public int AttackTick = 1000;
        public int TodesanimZeit = 0;
        public int stanned = 0;
        public int radius = 10;
        public bool splayed = false;
        public int stage = 0;
        public bool teleport = true;
        public BlockPos spawnpos = null;
        public bool playsoundagain = true;

        ICoreAPI aPi;

        public override void Initialize(EntityProperties properties, ICoreAPI api, long InChunkIndex3d)
        {
            base.Initialize(properties, api, InChunkIndex3d);
            //Properties.KnockbackResistance = 10;
            ICoreAPI aPi = api;
        }

        public override void OnEntitySpawn()
        {
            base.OnEntitySpawn();
            teleport = true;
        }

        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);


            if (Alive == true)
            {
                if (this.Alive == true)
                {
                    if (TestPlayersNearby() == false)
                    {
                        if (this.spawnpos != null)
                        {
                            this.World.BlockAccessor.SetBlock(this.World.BlockAccessor.GetBlock(new AssetLocation("omniummod:altar-clear")).BlockId, this.spawnpos);
                        }
                        this.ServerPos.Y = -20;
                        this.Pos.Y = -20;
                        playsoundagain = false;
                        Die(EnumDespawnReason.Unload);
                    }

                    if (splayed == false)
                    {
                        splayed = true;
                        music();
                    }

                    if (AttackTick == 50)
                    {
                        //if (this.teleport == true)
                        //{
                        try
                        {
                            Random r = new Random();
                            Random r2 = new Random();
                            Random r3 = new Random();
                            Random r4 = new Random();
                            EntityPos e = new EntityPos(
                                Convert.ToDouble(r.Next(Convert.ToInt32(spawnpos.X) - radius, Convert.ToInt32(spawnpos.X) + radius)),
                                Convert.ToDouble(r2.Next(Convert.ToInt32(spawnpos.Y + 2), Convert.ToInt32(spawnpos.Y) + (radius / 2))),
                                Convert.ToDouble(r3.Next(Convert.ToInt32(spawnpos.Z) - radius, Convert.ToInt32(spawnpos.Z) + radius)));
                            SimpleParticleProperties myParticles = new SimpleParticleProperties(300, 300, ColorUtil.ColorFromRgba(0, 0, 0, 0), Pos.XYZ.AddCopy(-5, -5, -5), Pos.XYZ.AddCopy(5, 5, 5), new Vec3f(-1, -1, -1), new Vec3f(1, 1, 1), 1, 0, 1, 3);
                            World.SpawnParticles(myParticles);
                            TeleportTo(e);
                        }
                        catch { }
                        //}
                        AttackTick = 1000;
                    }       //---Teleport

                    if (AttackTick == 100)
                    {
                        if (stage == 0)
                        {
                            for (int a = 0; a <= 10; a++)
                            {
                                EntityProperties type = this.World.GetEntityType(new AssetLocation("omniummod:soulpr"));
                                Entity ent = this.World.ClassRegistry.CreateEntity(type);
                                ent.ServerPos.SetPos((double)World.Rand.Next((int)this.ServerPos.X - 3, (int)this.ServerPos.X + 4), this.ServerPos.Y + 4.5, (double)World.Rand.Next((int)this.ServerPos.Z - 3, (int)this.ServerPos.Z + 4));
                                ent.ServerPos.Motion.Set(0, 0, 0);
                                ent.Pos.SetFrom(ent.ServerPos);
                                World.SpawnEntity(ent);
                            }
                        }
                        if (stage == 1)
                        {
                            for (int a = 0; a <= 20; a++)
                            {
                                EntityProperties type = this.World.GetEntityType(new AssetLocation("omniummod:soulpr"));
                                Entity ent = this.World.ClassRegistry.CreateEntity(type);
                                ent.ServerPos.SetPos((double)World.Rand.Next((int)this.ServerPos.X - 3, (int)this.ServerPos.X + 4), this.ServerPos.Y + 4.5, (double)World.Rand.Next((int)this.ServerPos.Z - 3, (int)this.ServerPos.Z + 4));
                                ent.ServerPos.Motion.Set(0, 0, 0);
                                ent.Pos.SetFrom(ent.ServerPos);
                                World.SpawnEntity(ent);
                            }
                        }
                        AttackTick = 1000;

                    }       //---Summon Soul

                    if (AttackTick == 150)
                    {
                        SimpleParticleProperties myParticles = new SimpleParticleProperties(30, 50, ColorUtil.ColorFromRgba(255, 255, 0, 0), Pos.XYZ.AddCopy(-5, 2, -5), Pos.XYZ.AddCopy(5, 2, 5), new Vec3f(-5, 2, -5), new Vec3f(5, 2, 5), 1, 0, 1, 3);
                        World.SpawnParticles(myParticles);
                    }       //---Spawn Shoot BossPrOne particles 1

                    if (AttackTick == 200)
                    {
                        SimpleParticleProperties myParticles = new SimpleParticleProperties(200, 300, ColorUtil.ColorFromRgba(255, 255, 0, 0), Pos.XYZ.AddCopy(-5, 2, -5), Pos.XYZ.AddCopy(5, 2, 5), new Vec3f(-5, 2, -5), new Vec3f(5, 2, 5), 1, 0, 1, 3);
                        World.SpawnParticles(myParticles);
                    }       //---Spawn Shoot BossPrOne particles 2

                    if (AttackTick == 250)
                    {
                        shoot("1");
                        AttackTick = 1000;
                    }       //---Shoot BossPrOne

                    if (AttackTick == 300)      //-----Spawn iceblocks
                    {
                        SpawnIceblock();
                        AttackTick = 1000;
                    }       //---Spawn Iceblock

                    if (AttackTick >= 350 && AttackTick <= 420)      //-----Circleshooting
                    {
                        try
                        {
                            if (AttackTick == 350)
                            {
                                EntityPos e = new EntityPos(
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.X) - 10),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Y + 1)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Z))
                                    );
                                TeleportTo(e);
                                shoot("2");
                            }
                            if (AttackTick == 360)
                            {
                                EntityPos e = new EntityPos(
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.X) - 9),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Y + 1)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Z) - 9)
                                    );
                                TeleportTo(e);
                                shoot("2");
                            }
                            if (AttackTick == 370)
                            {
                                EntityPos e = new EntityPos(
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.X)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Y + 1)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Z) - 10)
                                    );
                                TeleportTo(e);
                                shoot("2");
                            }
                            if (AttackTick == 380)
                            {
                                EntityPos e = new EntityPos(
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.X) + 9),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Y + 1)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Z) - 9)
                                    );
                                TeleportTo(e);
                                shoot("2");
                            }
                            if (AttackTick == 390)
                            {
                                EntityPos e = new EntityPos(
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.X) + 10),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Y + 1)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Z))
                                    );
                                TeleportTo(e);
                                shoot("2");
                            }
                            if (AttackTick == 400)
                            {
                                EntityPos e = new EntityPos(
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.X) + 9),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Y + 1)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Z) + 9)
                                    );
                                TeleportTo(e);
                                shoot("2");
                            }
                            if (AttackTick == 410)
                            {
                                EntityPos e = new EntityPos(
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.X)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Y + 1)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Z) + 10)
                                    );
                                TeleportTo(e);
                                shoot("2");
                            }
                            if (AttackTick == 420)
                            {
                                EntityPos e = new EntityPos(
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.X) - 9),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Y + 1)),
                                    Convert.ToDouble(Convert.ToInt32(spawnpos.Z) + 9)
                                    );
                                TeleportTo(e);
                                shoot("2");
                                AttackTick = 1000;
                            }
                        }
                        catch { }
                    }       //---Spawn Iceblock


                    AttackTick += 1;

                    if (AttackTick == 1100)
                    {
                        int i = World.Rand.Next(0, 6);
                        if (i == 0) AttackTick = 50;
                        if (i == 1) AttackTick = 100;
                        if (i == 2) AttackTick = 150;
                        if (i == 3) AttackTick = 300;
                        if (i == 4) AttackTick = 300;
                        if (i == 5) AttackTick = 350;
                    }


                }
            }
            else
            {
                TodesanimZeit += 1;
                this.Pos.Motion = new Vec3d(0, 0.01, 0);
                if (TodesanimZeit == 500)
                {
                    AssetLocation sound = new AssetLocation("omniummod", "sounds/entity/land/bosses/explode");
                    if (World.NearestPlayer(this.Pos.X, this.Pos.Y, this.Pos.Z) != null) World.PlaySoundAt(sound, this.Pos.X, this.Pos.Y, this.Pos.Z, World.NearestPlayer(this.Pos.X, this.Pos.Y, this.Pos.Z), EnumSoundType.Sound, 1, 100, 1);
                    SimpleParticleProperties myParticles = new SimpleParticleProperties(1000, 2000, ColorUtil.ColorFromRgba(255, 255, 0, 0), ServerPos.XYZ.AddCopy(-10, -10, -10), ServerPos.XYZ.AddCopy(10, 10, 10), new Vec3f(-5, 2, -5), new Vec3f(5, 2, 5), 1, 0, 1, 3);
                    World.SpawnParticles(myParticles);

                    if (this.spawnpos != null)
                    {
                        this.World.BlockAccessor.SetBlock(this.World.BlockAccessor.GetBlock(new AssetLocation("omniummod:altar-clear")).BlockId, this.spawnpos);
                    }

                    this.ServerPos.Y = -20;
                    this.Pos.Y = -20;
                    this.Die(EnumDespawnReason.Removed);
                }
            }
        }

        public override bool ReceiveDamage(DamageSource damageSource, float damage)
        {
            return base.ReceiveDamage(damageSource, damage);
            Pos.Motion.X = 0;
            Pos.Motion.Y = 0;
            Pos.Motion.Z = 0;
            this.controls.FlyVector = new Vintagestory.API.MathTools.Vec3d(0, 0, 0);
            this.controls.WalkVector = new Vintagestory.API.MathTools.Vec3d(0, 0, 0);
            stanned = 10;
        }

        public override void OnStateChanged(EnumEntityState beforeState)
        {
            base.OnStateChanged(beforeState);
            if (this.spawnpos != null)
            {
                this.World.BlockAccessor.SetBlock(this.World.BlockAccessor.GetBlock(new AssetLocation("omniummod:altar-clear")).BlockId, this.spawnpos);
            }
            playsoundagain = false;
            Die(EnumDespawnReason.Unload);
        }

        public void shoot(string t)
        {
            if (t == "1")
            {
                float damage = 30;
                Entity targetEntity = null;
                string[] seekEntityCodesExact = new string[1] { "player" };
                targetEntity = World.GetNearestEntity(Pos.XYZ, 80f, 80f, (ActionConsumable<Entity>)(e =>
                {
                    if (!e.Alive || !e.IsInteractable || e.EntityId == EntityId)
                        return false;
                    if (e.Code.Path == "player")
                    {
                        IPlayer player = World.PlayerByUid(((EntityPlayer)e).PlayerUID);
                        return true;
                    }
                    return false;
                }));

                if (targetEntity != null)
                {
                    EntityProperties type = this.World.GetEntityType(new AssetLocation("omniummod:bossprone"));
                    Entity entity = World.ClassRegistry.CreateEntity(type);

                    float acc = Math.Max(0.001f, (1 - 1));

                    float num1 = (float)(targetEntity.ServerPos.X - ServerPos.X);
                    float num2 = (float)(targetEntity.ServerPos.Y - (ServerPos.Y + (LocalEyePos.Y - 0.2)));
                    float num3 = (float)(targetEntity.ServerPos.Z - ServerPos.Z);
                    ServerPos.Yaw += GameMath.AngleRadDistance(ServerPos.Yaw, (float)Math.Atan2((double)num1, (double)num3));
                    ServerPos.Yaw %= 6.283185f;

                    float pitch = ServerPos.Pitch;
                    float end = (float)Math.Atan2((double)num2, Math.Sqrt((double)num3 * (double)num3 + (double)num1 * (double)num1));
                    float num4 = GameMath.AngleRadDistance(pitch, end);
                    float num5 = (pitch + num4) % 6.283185f;
                    //ServerPos.Pitch = num5;

                    Vec3d vec3d = ServerPos.XYZ.Add(0.0, LocalEyePos.Y + 3, 0.0);
                    Vec3d pos = (vec3d.AheadCopy(2.5, num5, ServerPos.Yaw + 1.623156f) - vec3d);
                    Vec3d velocity = new Vec3d(pos.X, pos.Y, pos.Z);

                    entity.ServerPos.SetPos(SidedPos.BehindCopy(2.21).XYZ.Add(0, LocalEyePos.Y, 0));
                    entity.ServerPos.Motion.Set(new Vec3d(velocity.X / 6, velocity.Y / 15, velocity.Z / 6));

                    entity.Pos.SetFrom(entity.ServerPos);
                    entity.World = World;

                    World.SpawnEntity(entity);

                }
            }
            if (t == "2")
            {
                try
                {
                    float damage = 30;
                    EntityProperties type = this.World.GetEntityType(new AssetLocation("omniummod:bossprsecond"));
                    Entity entity = World.ClassRegistry.CreateEntity(type);

                    float acc = Math.Max(0.001f, (1 - 1));

                    float num1 = (float)(spawnpos.X - ServerPos.X);
                    float num2 = (float)(spawnpos.Y - (ServerPos.Y + (LocalEyePos.Y - 0.2)));
                    float num3 = (float)(spawnpos.Z - ServerPos.Z);
                    ServerPos.Yaw += GameMath.AngleRadDistance(ServerPos.Yaw, (float)Math.Atan2((double)num1, (double)num3));
                    ServerPos.Yaw %= 6.283185f;

                    float pitch = ServerPos.Pitch;
                    float end = (float)Math.Atan2((double)num2, Math.Sqrt((double)num3 * (double)num3 + (double)num1 * (double)num1));
                    float num4 = GameMath.AngleRadDistance(pitch, end);
                    float num5 = (pitch + num4) % 6.283185f;
                    //ServerPos.Pitch = num5;

                    Vec3d vec3d = ServerPos.XYZ.Add(0.0, LocalEyePos.Y + 3, 0.0);
                    Vec3d pos = (vec3d.AheadCopy(2.5, num5, ServerPos.Yaw + 1.623156f) - vec3d);
                    Vec3d velocity = new Vec3d(pos.X, pos.Y, pos.Z);

                    entity.ServerPos.SetPos(SidedPos.BehindCopy(2.21).XYZ.Add(0, LocalEyePos.Y, 0));
                    entity.ServerPos.Motion.Set(new Vec3d(velocity.X / 12, velocity.Y / 30, velocity.Z / 12));

                    entity.Pos.SetFrom(entity.ServerPos);
                    entity.World = World;

                    World.SpawnEntity(entity);
                }
                catch { }
            }
        }

        public void SpawnIceblock()
        {
            //try
            //{
            IPlayer[] players = World.GetPlayersAround(this.ServerPos.XYZ, 300, 300, null);

            foreach (IPlayer p in players)
            {
                EntityProperties type = this.World.GetEntityType(new AssetLocation("omniummod:iceblockpr"));
                Entity entity = World.ClassRegistry.CreateEntity(type);

                entity.ServerPos.SetPos(p.Entity.ServerPos.XYZ.Add(0, LocalEyePos.Y + 3, 0));
                entity.ServerPos.Motion.Set(0, 0, 0);

                entity.Pos.SetFrom(entity.ServerPos);
                entity.World = World;

                World.SpawnEntity(entity);
            }
            //}
            //catch { }
        }

        public override void OnEntityDespawn(EntityDespawnReason despawn)
        {
            base.OnEntityDespawn(despawn);

            if (this.spawnpos != null)
            {
                this.World.BlockAccessor.SetBlock(this.World.BlockAccessor.GetBlock(new AssetLocation("omniummod:altar-clear")).BlockId, this.spawnpos);
            }
            playsoundagain = false;
            Die(EnumDespawnReason.Unload);
        }

        public void music()
        {
            
            AssetLocation sound = new AssetLocation("omniummod", "sounds/entity/land/bosses/bossmusik");
            if (World.NearestPlayer(this.Pos.X, this.Pos.Y, this.Pos.Z) != null)
                World.PlaySoundAt(sound, this.Pos.X, this.Pos.Y, this.Pos.Z, World.NearestPlayer(this.Pos.X, this.Pos.Y, this.Pos.Z), EnumSoundType.SoundGlitchunaffected, 1, 100, 1);//this.World.PlaySoundAt(sound, this, null, false, 100, 1);
            if (Alive == true && playsoundagain == true) World.RegisterCallback((dt) =>
            {
                if (this.Alive == true && playsoundagain == true) { music(); }
            }, 151000);// 151000
        }

        public bool TestPlayersNearby()
        {
            IPlayer[] players = World.GetPlayersAround(this.Pos.XYZ, 3000, 3000, null);
            foreach (IPlayer p in players)
            {
                if (p.Entity.Alive == true) return true;
            }
            return false;
        }
    }

    public class bossprone : EntityAgent
    {
        Entity DamageEntity = null;


        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);

            Pos.Pitch = 0;
            Pos.Yaw = 0;
            Pos.Roll = 0;

            Vec3d pos = ServerPos.XYZ;
            SimpleParticleProperties myParticles = new SimpleParticleProperties(5, 20, ColorUtil.ColorFromRgba(255, 255, 0, 0), pos.AddCopy(-0.2, -0.2, -0.2), pos.AddCopy(0.2, -0.2, 0.2), new Vec3f(-1.5f, -1.5f, -1.5f), new Vec3f(1.5f, 1.5f, 1.5f), 1, 0, 1, 3);
            World.SpawnParticles(myParticles);

            IPlayer[] e = World.GetPlayersAround(ServerPos.XYZ, 3f, 3f);
            Entity entity = null;
            try { entity = e[0].Entity; } catch { }

            if (entity != null)
            {
                entity.ReceiveDamage(new DamageSource()
                {
                    Source = EnumDamageSource.Entity,
                    SourceEntity = this,
                    Type = EnumDamageType.Suffocation
                }, 10f);
                Die();
            }
        }

        public override void OnCollided()
        {
            base.OnCollided();

            //Api.World.BlockAccessor.SetBlock(Api.World.BlockAccessor.GetBlock(new AssetLocation("game:air")).BlockId, new BlockPos((int)Pos.X, (int)Pos.Y, (int)Pos.Z));

            /*EntityPos pos = SidedPos;

            IPlayer[] e = World.GetPlayersAround(ServerPos.XYZ, 300f, 300f);
            Entity entity = null;
            try { entity = e[0].Entity; } catch { }
            
            if (entity != null)
            {
                entity.ReceiveDamage(new DamageSource()
                {
                    Source = EnumDamageSource.Entity,
                    SourceEntity = this,
                    Type = EnumDamageType.Suffocation
                }, 30f);
                World.BlockAccessor.SetBlock(10, new BlockPos((int)Pos.X, (int)(Pos.Y), (int)Pos.Z));
                Die();
            }
            //GiveDamage(entity);*/
            //DamageEntity.ReceiveDamage(new DamageSource() { SourceEntity = this, Type = EnumDamageType.SlashingAttack }, 30f);


        }

    }

    public class bossprsecond : EntityAgent
    {
        Entity DamageEntity = null;


        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);

            Pos.Pitch = 0;
            Pos.Yaw = 0;
            Pos.Roll = 0;

            Vec3d pos = ServerPos.XYZ;
            SimpleParticleProperties myParticles = new SimpleParticleProperties(2, 2, ColorUtil.ColorFromRgba(255, 255, 0, 0), pos.AddCopy(-0.2, -0.2, -0.2), pos.AddCopy(0.2, -0.2, 0.2), new Vec3f(0f, 0f, 0f), new Vec3f(0f, 0f, 0f), 0.2f, 0, 1, 3);
            SimpleParticleProperties myParticles2 = new SimpleParticleProperties(10, 10, ColorUtil.ColorFromRgba(255, 255, 0, 0), pos.AddCopy(-0.2, -0.2, -0.2), pos.AddCopy(0.2, -0.2, 0.2), new Vec3f(0f, 0f, 0f), new Vec3f(0f, 0f, 0f), 0.2f, 0, 1, 3);
            World.SpawnParticles(myParticles);
            World.SpawnParticles(myParticles2);

            IPlayer[] e = World.GetPlayersAround(ServerPos.XYZ, 1.5f, 1.5f);
            Entity entity = null;
            try { entity = e[0].Entity; } catch { }

            if (entity != null)
            {
                entity.ReceiveDamage(new DamageSource()
                {
                    Source = EnumDamageSource.Entity,
                    SourceEntity = this,
                    Type = EnumDamageType.Suffocation
                }, 5f);
                Die();
            }
        }


    }

    public class IceblockPr : EntityAgent
    {
        EntityPos spawnpos = new EntityPos(0, 0, 0, 0, 0, 0);
        int time = 0;

        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);

            if (time == 0) spawnpos = new EntityPos(this.Pos.X, this.Pos.Y, this.Pos.Z, this.Pos.Yaw, this.Pos.Pitch, this.Pos.Roll);

            if (time <= 20)
            {
                time += 1;
                this.Pos.Motion = new Vec3d(0, 0, 0);
                this.ServerPos.Motion = new Vec3d(0, 0, 0);
            }
            else
            {
                IPlayer[] p = World.GetPlayersAround(ServerPos.XYZ, 1f, 1f);
                Entity entity = null;
                try { entity = p[0].Entity; } catch { }

                if (entity != null)
                {
                    entity.ReceiveDamage(new DamageSource()
                    {
                        Source = EnumDamageSource.Entity,
                        SourceEntity = this,
                        Type = EnumDamageType.Suffocation
                    }, 10f);
                    World.BlockAccessor.SetBlock(World.BlockAccessor.GetBlock(new AssetLocation("omniummod:iceblock-buttom")).Id, new BlockPos((int)ServerPos.X, (int)ServerPos.Y, (int)ServerPos.Z));
                    World.BlockAccessor.SetBlock(World.BlockAccessor.GetBlock(new AssetLocation("omniummod:iceblock-top")).Id, new BlockPos((int)ServerPos.X, (int)ServerPos.Y + 1, (int)ServerPos.Z));
                    AssetLocation sound = new AssetLocation("game", "sounds/block/glass");
                    World.PlaySoundAt(sound, (double)ServerPos.X, (double)ServerPos.Y, (double)ServerPos.Z);
                    this.Die(EnumDespawnReason.Removed);
                }
            }

        }

        public override void OnCollided()
        {
            base.OnCollided();

            IPlayer[] p = World.GetPlayersAround(ServerPos.XYZ, 1f, 1f);
            Entity entity = null;
            try { entity = p[0].Entity; } catch { }

            if (entity != null)
            {
                entity.ReceiveDamage(new DamageSource()
                {
                    Source = EnumDamageSource.Entity,
                    SourceEntity = this,
                    Type = EnumDamageType.Suffocation
                }, 10f);
            }
            if (World.BlockAccessor.GetBlock(new BlockPos((int)ServerPos.X, (int)ServerPos.Y - 1, (int)ServerPos.Z)).Id != 0 && World.BlockAccessor.GetBlock(new BlockPos((int)ServerPos.X, (int)ServerPos.Y - 1, (int)ServerPos.Z)).LiquidCode != "water")
            {
                World.BlockAccessor.SetBlock(World.BlockAccessor.GetBlock(new AssetLocation("omniummod:iceblock-buttom")).Id, new BlockPos((int)ServerPos.X, (int)ServerPos.Y, (int)ServerPos.Z));
                World.BlockAccessor.SetBlock(World.BlockAccessor.GetBlock(new AssetLocation("omniummod:iceblock-top")).Id, new BlockPos((int)ServerPos.X, (int)ServerPos.Y + 1, (int)ServerPos.Z));
                AssetLocation sound = new AssetLocation("game", "sounds/block/glass");
                World.PlaySoundAt(sound, (double)ServerPos.X, (double)ServerPos.Y, (double)ServerPos.Z);
            }
            this.Die(EnumDespawnReason.Removed);
        }
    }

    public class Worm : EntityAgent
    {
        public EntityPos PreviousPos = new EntityPos();
        public float segmenLength = 1;
        public int segmentcount = 10;
        public Entity[] Segments = new Entity[10];
        int time = 0;


        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);
            time++;
            if (time >= 1)
            {
                try
                {
                    EntityPos pos = new EntityPos(Pos.X, Pos.Y, Pos.Z, 0, Pos.Pitch, Pos.Roll);


                    float acc2 = Math.Max(0.001f, (1 - 1));

                    float num12 = (float)(Segments[0].Pos.X - pos.X);
                    //float num22 = (float)(Segments[0].ServerPos.Y - (this.ServerPos.Y + (this.LocalEyePos.Y - 0.2)));
                    float num32 = (float)(Segments[0].Pos.Z - pos.Z);
                    pos.Yaw += GameMath.AngleRadDistance(pos.Yaw, (float)Math.Atan2((double)num12, (double)num32));
                    pos.Yaw %= 6.283185f;

                    Segments[0].ServerPos = pos.AheadCopy(segmenLength * 3);

                    for (int a = 1; a <= Segments.Length - 1; a++)
                    {
                        float acc = Math.Max(0.001f, (1 - 1));

                        float num1 = (float)(Segments[a].Pos.X - Segments[a - 1].Pos.X);
                        //float num2 = (float)(Segments[a].ServerPos.Y - (Segments[a - 1].ServerPos.Y + (Segments[a].LocalEyePos.Y - 0.2)));
                        float num3 = (float)(Segments[a].Pos.Z - Segments[a - 1].Pos.Z);
                        Segments[a - 1].Pos.Yaw += GameMath.AngleRadDistance(Segments[a - 1].Pos.Yaw, (float)Math.Atan2((double)num1, (double)num3));
                        Segments[a - 1].Pos.Yaw %= 6.283185f;

                        Segments[a].ServerPos = Segments[a - 1].Pos.AheadCopy(segmenLength * 3);
                    }

                }
                catch { }
                time = 0;
            }
        }
    }

    public class Mastermade : Worm
    {
        //  /entity spawn omniummod:mastermade
        public override void OnEntitySpawn()
        {
            base.OnEntitySpawn();

            for (int a = 0; a <= 9; a++)
            {
                EntityProperties type = this.World.GetEntityType(new AssetLocation("omniummod:souldrinker"));
                Entity entity = World.ClassRegistry.CreateEntity(type);
                EntityPos pos = this.ServerPos.BehindCopy(a * segmenLength);
                pos.Y += 0.5;

                entity.ServerPos.SetPos(pos);
                entity.ServerPos.Motion.Set(0, 0, 0);

                entity.Pos.SetFrom(entity.ServerPos);
                entity.World = World;

                World.SpawnEntity(entity);
                this.Segments[a] = entity;
            }
        }
    }

    //---------------Blocks

    public class hydrothermal_vent : Block
    {
        public override bool TryPlaceBlockForWorldGen(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand)
        {
            base.TryPlaceBlockForWorldGen(blockAccessor, pos, onBlockFace, worldGenRand);

            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);

            BlockPos belowPos = pos.Down();
            bool place = false;
            Block block = blockAccessor.GetBlock(belowPos);
            if (block.LiquidCode != "water") return false;

            int depth = 1;

            while (depth < 200)
            {
                belowPos.Down();
                block = blockAccessor.GetBlock(belowPos);
                if (block.LiquidCode == "water") { place = true; }
                if (block.LiquidCode != "water" && place == true && depth >= 80)
                {
                    belowPos.Up();
                    PlacePlate(blockAccessor, belowPos);
                    return true;
                }
                depth++;
            }
            return false;
        }

        public void PlacePlate(IBlockAccessor blockAccessor, BlockPos pos)
        {
            BlockPos p = pos;
            BlockPos po = pos;

            for (int a = 0; a <= 4; a++)
            {
                int z = api.World.Rand.Next(1, 6);
                for (int b = 0; b <= z; b++)
                {
                    PlaceVent(blockAccessor, p);
                    p.Up();
                }
                for (int c = 0; c <= z; c++) { p.Down(); }
                int z2 = api.World.Rand.Next(0, 4);
                if (z2 == 0) p.North();
                if (z2 == 1) p.East();
                if (z2 == 2) p.South();
                if (z2 == 3) p.West();
            }
            for (int b = 0; b <= 2; b++) { p.West(); }
            for (int a = 0; a <= 6; a++)
            {
                for (int c = 0; c <= 2; c++) { p.North(); }
                for (int d = 0; d <= 6; d++) { p.South(); PlaceIronsulfid(blockAccessor, p); }
                for (int e = 0; e <= 3; e++) { p.North(); }
                p.East();
            }
            for (int b = 0; b <= 4; b++) { p.West(); }
            p.Up();
            for (int b = 0; b <= 4; b++) { p.West(); }
            for (int a = 0; a <= 10; a++)
            {
                for (int c = 0; c <= 4; c++) { p.North(); }
                for (int d = 0; d <= 10; d++) { p.South(); PlaceWormClam(blockAccessor, p); }
                for (int e = 0; e <= 5; e++) { p.North(); }
                p.East();
            }
        }

        public void PlaceVent(IBlockAccessor blockAccessor, BlockPos pos)
        {
            BlockPos p = pos;
            int depth = 0;
            Block block = blockAccessor.GetBlock(p);
            while (depth < 200)
            {
                p.Down();
                block = blockAccessor.GetBlock(p);
                if (block.LiquidCode != "water")
                {
                    p.Up();
                    blockAccessor.SetBlock(BlockId, p);
                    break;
                }
                depth++;
            }
        }

        public void PlaceWormClam(IBlockAccessor blockAccessor, BlockPos pos)
        {
            BlockPos p = pos;
            int depth = 0;
            int z = api.World.Rand.Next(0, 3);
            Block block = blockAccessor.GetBlock(p);
            while (depth < 200)
            {
                p.Down();
                block = blockAccessor.GetBlock(p);
                if (block.LiquidCode != "water")
                {
                    for (int a = 0; a <= 10; a++)
                    {
                        p.Up();
                        block = blockAccessor.GetBlock(p);
                        if (block.LiquidCode == "water")
                        {
                            if (z == 0) blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:deepworm")).BlockId, p);
                            if (z == 1) blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:deepclam")).BlockId, p);
                            break;
                        }
                    }
                    break;
                }
                depth++;
            }
        }

        public void PlaceIronsulfid(IBlockAccessor blockAccessor, BlockPos pos)
        {
            BlockPos p = pos;
            int depth = 0;
            int z = api.World.Rand.Next(0, 3);
            Block block = blockAccessor.GetBlock(p);
            while (depth < 200)
            {
                p.Down();
                block = blockAccessor.GetBlock(p);
                if (block.LiquidCode != "water")
                {
                    p.Up();
                    if (z <= 1) blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:ironsulfid")).BlockId, p);
                    break;
                }
                depth++;
            }
        }


    }

    public class Mushroomrootedstone : Block
    {
        public override bool TryPlaceBlockForWorldGen(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand)
        {
            base.TryPlaceBlockForWorldGen(blockAccessor, pos, onBlockFace, worldGenRand);

            int i = worldGenRand.NextInt(60);
            i = i + 40;
            
            int radius = 0;
            if (pos.Y - i > 10 && pos.Y - i < 60) 
            {
                int steinid = blockAccessor.GetBlock(new AssetLocation("omniummod:mushroomrootedstone-basalt")).Id;
                for (int a = 0; a <= 10; a++)
                {
                    try
                    {
                        steinid = blockAccessor.GetBlock(new AssetLocation("omniummod:mushroomrootedstone-" + blockAccessor.GetBlock(new BlockPos(pos.X, pos.Y - a - i, pos.Z)).Variant["rock"].ToString())).Id;
                        break;
                    }
                    catch { }
                }

                radius = GenerateCave(blockAccessor, new BlockPos(pos.X, pos.Y - i, pos.Z), onBlockFace, worldGenRand, steinid);
                GenerateFungus(blockAccessor, new BlockPos(pos.X, pos.Y - i, pos.Z), onBlockFace, worldGenRand, radius, steinid);
            }
            else { return false; }

            return true;
        }

        public int GenerateCave(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand, int msid)
        {
            int radius = worldGenRand.NextInt(15);
            if (radius < 7) radius = 7;
            BlockPos GPos = new BlockPos(pos.X, pos.Y, pos.Z);

            for (int a = -radius; a <= radius + 1; a++)
            {
                for (int b = -radius; b <= radius + 1; b++)
                {
                    for (int c = 0; c <= radius + 1; c++)
                    {
                        blockAccessor.SetBlock(0, new BlockPos(GPos.X + a, GPos.Y + c, GPos.Z + b));
                    }
                }
            }
            int steinid = msid;
            
            for (int b = 0; b <= radius + 1; b++)
            {
                int l = Convert.ToInt32((float)radius * (5f / 7f));
                l += Convert.ToInt32((float)b / 3f);
                for (int a = 0; a <= l; a++)
                {
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + a, GPos.Y + b, GPos.Z - radius));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius, GPos.Y + b, GPos.Z - radius + a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius + 1 - a, GPos.Y + b, GPos.Z - radius));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius + 1, GPos.Y + b, GPos.Z - radius + a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius + 1 - a, GPos.Y + b, GPos.Z + radius + 1));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius + 1, GPos.Y + b, GPos.Z + radius + 1 - a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + a, GPos.Y + b, GPos.Z + radius + 1));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius, GPos.Y + b, GPos.Z + radius + 1 - a));
                }
                l = Convert.ToInt32((float)radius * (3f / 7f));
                l += Convert.ToInt32((float)b / 3f);
                for (int a = 0; a <= l; a++)
                {
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + a, GPos.Y + b, GPos.Z - radius + 1));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + 1, GPos.Y + b, GPos.Z - radius + a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius + 1 - a, GPos.Y + b, GPos.Z - radius + 1));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius, GPos.Y + b, GPos.Z - radius + a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius + 1 - a, GPos.Y + b, GPos.Z + radius));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius, GPos.Y + b, GPos.Z + radius + 1 - a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + a, GPos.Y + b, GPos.Z + radius));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + 1, GPos.Y + b, GPos.Z + radius + 1 - a));
                }
                l = Convert.ToInt32((float)radius * (2f / 7f));
                l += Convert.ToInt32((float)b / 3f);
                for (int a = 0; a <= l; a++)
                {
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + a, GPos.Y + b, GPos.Z - radius + 2));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + 2, GPos.Y + b, GPos.Z - radius + a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius + 1 - a, GPos.Y + b, GPos.Z - radius + 2));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius - 1, GPos.Y + b, GPos.Z - radius + a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius + 1 - a, GPos.Y + b, GPos.Z + radius - 1));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X + radius - 1, GPos.Y + b, GPos.Z + radius + 1 - a));

                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + a, GPos.Y + b, GPos.Z + radius - 1));
                    blockAccessor.SetBlock(steinid, new BlockPos(GPos.X - radius + 2, GPos.Y + b, GPos.Z + radius + 1 - a));
                }
            }

            return radius;
        }

        public void GenerateFungus(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand, int radius, int msid)
        {
            int klPilzId = blockAccessor.GetBlock(new AssetLocation("game:mushroom-höhlenpilz-normal-free")).Id;
            int MushroomStoneId = msid;

            for (int a = -radius; a <= radius + 1; a++)
            {
                for (int b = -radius; b <= radius + 1; b++)
                {
                    BlockPos GPos = new BlockPos(pos.X + a, pos.Y, pos.Z + b);
                    if (blockAccessor.GetBlock(GPos.Up()).Id == 0)
                    {
                        int i = worldGenRand.NextInt(64);
                        if (i < 31)
                        {
                            int yoffset = worldGenRand.NextInt(4);
                            if (yoffset == 0 || yoffset == 1)
                            {
                                GPos.Down();
                                blockAccessor.SetBlock(klPilzId, GPos);
                                GPos.Down();
                                blockAccessor.SetBlock(MushroomStoneId, GPos);
                            }
                            if (yoffset == 2)
                            {
                                blockAccessor.SetBlock(klPilzId, GPos);
                                GPos.Down();
                                blockAccessor.SetBlock(MushroomStoneId, GPos);
                            }
                            if (yoffset == 3)
                            {
                                GPos.Down(); GPos.Down();
                                blockAccessor.SetBlock(klPilzId, GPos);
                                GPos.Down();
                                blockAccessor.SetBlock(MushroomStoneId, GPos);
                            }
                        }

                        if (i == 63)
                        {
                            GenerateGiantFungus(blockAccessor, GPos.Down(), onBlockFace, worldGenRand);
                        }

                        if (i >= 50 && i <= 62)
                        {
                            GenerateLianes(blockAccessor, GPos, onBlockFace, worldGenRand, radius);
                        }

                    }
                }
            }
        }

        public void GenerateGiantFungus(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand)
        {
            int GrPilzHutId = blockAccessor.GetBlock(new AssetLocation("omniummod:mushroomblock-head")).Id;
            int GrPilzStammId = blockAccessor.GetBlock(new AssetLocation("omniummod:mushroomblock-stem")).Id;

            int height = worldGenRand.NextInt(5);
            height += 3;

            for (int a = 0; a <= height; a++)
            {
                blockAccessor.SetBlock(GrPilzStammId, new BlockPos(pos.X, pos.Y + a, pos.Z));
            }
            int heatwidth = worldGenRand.NextInt(2);
            heatwidth += 1;
            for (int a = -heatwidth; a <= heatwidth; a++)
            {
                for (int b = -heatwidth; b <= heatwidth; b++)
                {
                    blockAccessor.SetBlock(GrPilzHutId, new BlockPos(pos.X + a, pos.Y + height + 1, pos.Z + b));
                }
            }

            int headdown = worldGenRand.NextInt(2);
            headdown += 1;

            for (int a = -heatwidth; a <= heatwidth; a++)
            {
                for (int b = 0; b <= headdown; b++)
                {
                    blockAccessor.SetBlock(GrPilzHutId, new BlockPos(pos.X + heatwidth + 1, pos.Y + height - b, pos.Z + a));
                    blockAccessor.SetBlock(GrPilzHutId, new BlockPos(pos.X - heatwidth - 1, pos.Y + height - b, pos.Z + a));
                    blockAccessor.SetBlock(GrPilzHutId, new BlockPos(pos.X + a, pos.Y + height - b, pos.Z + heatwidth + 1));
                    blockAccessor.SetBlock(GrPilzHutId, new BlockPos(pos.X + a, pos.Y + height - b, pos.Z - heatwidth - 1));
                }
            }
        }

        public void GenerateLianes(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand, int radius)
        {
            int SectionId = blockAccessor.GetBlock(new AssetLocation("omniummod:mushroomliane-section")).Id;
            int EndId = blockAccessor.GetBlock(new AssetLocation("omniummod:mushroomliane-end")).Id;

            int height = worldGenRand.NextInt(5);
            height += 3;

            for (int a = 0; a <= radius + 2; a++)
            {
                if (blockAccessor.GetBlock(new BlockPos(pos.X, pos.Y + a, pos.Z)).Id != 0)
                {
                    int l = worldGenRand.NextInt(4);
                    l += 3;

                    for (int b = 0; b <= l - 1; b++)
                    {
                        if (blockAccessor.GetBlock(new BlockPos(pos.X, pos.Y + (a - b), pos.Z)).Id == 0)
                            blockAccessor.SetBlock(SectionId, new BlockPos(pos.X, pos.Y + (a - b), pos.Z));
                    }
                    if (blockAccessor.GetBlock(new BlockPos(pos.X, pos.Y + (a - l), pos.Z)).Id == 0)
                        blockAccessor.SetBlock(EndId, new BlockPos(pos.X, pos.Y + (a - l), pos.Z));
                    break;
                }
            }
        }

    }

    public class Coral : Block
    {
        public override bool TryPlaceBlockForWorldGen(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand)
        {
            base.TryPlaceBlockForWorldGen(blockAccessor, pos, onBlockFace, worldGenRand);

            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);

            BlockPos belowPos = pos.Down();
            bool place = false;
            Block block = blockAccessor.GetBlock(belowPos);
            if (block.LiquidCode != "water") return false;

            int depth = 1;

            while (depth < 200)
            {
                belowPos.Down();
                block = blockAccessor.GetBlock(belowPos);
                if (block.LiquidCode == "water") { place = true; }
                if (block.LiquidCode != "water" && place == true && block.BlockId != BlockId && depth >= 10 && depth <= 60)
                {
                    belowPos.Up();
                    PlaceCoral(blockAccessor, belowPos);
                    return true;
                }
                depth++;
            }
            return false;
        }

        public void PlaceRiff(IBlockAccessor blockAccessor, BlockPos pos)
        {
            BlockPos belowPos = pos.Down();
            BlockPos po = pos.Down();
            for (int a = 0; a <= 250; a++)
            {
                belowPos.X = api.World.Rand.Next(belowPos.X - 10, belowPos.X + 10);
                belowPos.Z = api.World.Rand.Next(belowPos.Z - 10, belowPos.Z + 10);


                bool place = false;
                Block block = blockAccessor.GetBlock(belowPos);

                int depth = 1;

                while (depth < 200)
                {
                    belowPos.Down();
                    block = blockAccessor.GetBlock(belowPos);
                    if (block.LiquidCode == "water") { place = true; }
                    if (block.LiquidCode != "water" && place == true && block.BlockId != BlockId)
                    {
                        belowPos.Up();
                        PlaceCoral(blockAccessor, belowPos);
                        break;
                    }
                    depth++;
                }


                belowPos = po;

            }
        }

        public void PlaceCoral(IBlockAccessor blockAccessor, BlockPos pos)
        {
            BlockPos p = pos.Down();
            BlockPos po = pos.Down();

            int Zeit = DateTime.Now.Millisecond;
            int Zeit2 = DateTime.Now.Millisecond;
            int Zeit3 = DateTime.Now.Millisecond;


            if (!blockAccessor.GetBlock(p).IsLiquid())
            {
                p.Up();
                po.Up();
                int type = 0;   //0 = cubic, 1 = long
                if (api.World.Rand.Next(0, 2) == 0) type = 0;
                else type = 1;
                string ctype = "black";
                int t = api.World.Rand.Next(0, 4);
                if (t == 3) ctype = "lps";  //---------Large Polyp Stony Coral
                if (t == 2) ctype = "bubble";
                if (t == 1) ctype = "brain";
                if (t == 0) ctype = "black";

                if (type == 0)//--------------------------------------massive cubic coral
                {
                    for (int a = 0; a <= 4; a++)
                    {

                        Zeit = PlaceCoralReal(blockAccessor, p, Zeit, a, ctype);
                        Zeit2 = DateTime.Now.Millisecond;
                        if (Zeit2 != Zeit3) { Zeit = DateTime.Now.Millisecond; Zeit2 = DateTime.Now.Millisecond; Zeit3 = DateTime.Now.Millisecond; }
                        for (int b = 0; b <= 3; b++)
                        {
                            p.West();
                            Zeit = PlaceCoralReal(blockAccessor, p, Zeit, a, ctype);
                            Zeit2 = DateTime.Now.Millisecond;
                            if (Zeit2 != Zeit3) { Zeit = DateTime.Now.Millisecond; Zeit2 = DateTime.Now.Millisecond; Zeit3 = DateTime.Now.Millisecond; }
                            for (int c = 0; c <= 3; c++)
                            {
                                p.South();
                                Zeit2 = DateTime.Now.Millisecond;
                                if (Zeit2 != Zeit3) { Zeit = DateTime.Now.Millisecond; Zeit2 = DateTime.Now.Millisecond; Zeit3 = DateTime.Now.Millisecond; }
                                Zeit = PlaceCoralReal(blockAccessor, p, Zeit, a, ctype);
                            }
                            for (int c1 = 0; c1 <= 3; c1++) p.North();
                        }
                        for (int b1 = 0; b1 <= 3; b1++) p.East();
                        p.Up();
                    }
                }

                if (type == 1)  //----------------------------long coral
                {
                    for (int a = 0; a <= 4; a++)
                    {

                        blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:coral-" + ctype)).BlockId, p);

                        int zahl = api.World.Rand.Next(0, 7);
                        if (zahl == 0)
                        {
                            int zahl2 = api.World.Rand.Next(1, 5);
                            for (a = 0; a <= zahl2; a++)
                            {
                                p.South();
                                blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:coral-" + ctype)).BlockId, p);
                            }
                            for (a = 0; a <= zahl2; a++) p.North();
                        }
                        if (zahl == 1)
                        {
                            int zahl2 = api.World.Rand.Next(1, 5);
                            for (a = 0; a <= zahl2; a++)
                            {
                                p.North();
                                blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:coral-" + ctype)).BlockId, p);
                            }
                            for (a = 0; a <= zahl2; a++) p.South();
                        }
                        if (zahl == 2)
                        {
                            int zahl2 = api.World.Rand.Next(1, 5);
                            for (a = 0; a <= zahl2; a++)
                            {
                                p.West();
                                blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:coral-" + ctype)).BlockId, p);
                            }
                            for (a = 0; a <= zahl2; a++) p.East();
                        }
                        if (zahl == 3)
                        {
                            int zahl2 = api.World.Rand.Next(1, 5);
                            for (a = 0; a <= zahl2; a++)
                            {
                                p.East();
                                blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:coral-" + ctype)).BlockId, p);
                            }
                            for (a = 0; a <= zahl2; a++) p.West();
                        }

                        p.Up();
                    }

                    blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:fern_coral-" + ctype)).BlockId, p);
                }


            }

        }

        public int PlaceCoralReal(IBlockAccessor blockAccessor, BlockPos pos, int Zeit, int a, string type)
        {
            BlockPos p = pos;
            int z = api.World.Rand.Next(0, 2000);
            if (z > 1000)
            {
                if (a <= 3) blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:coral-" + type)).BlockId, p);
                if (a >= 4)
                {
                    int z2 = api.World.Rand.Next(0, 100);
                    if (z2 > 75) blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:fern_coral-" + type)).BlockId, pos);
                }
            }
            Zeit += 1;
            if (Zeit > 1000) Zeit = 0;
            return Zeit;
        }

    }

    public class Giantkelp : Block
    {
        public override bool TryPlaceBlockForWorldGen(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand)
        {
            base.TryPlaceBlockForWorldGen(blockAccessor, pos, onBlockFace, worldGenRand);

            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);

            int z = api.World.Rand.Next(5, 20);

            BlockPos belowPos = pos.Down();
            bool place = false;
            Block block = blockAccessor.GetBlock(belowPos);
            if (block.LiquidCode != "water") return false;

            int depth = 1;

            while (depth < 200)
            {
                belowPos.Down();
                block = blockAccessor.GetBlock(belowPos);
                if (block.LiquidCode == "water") { place = true; }
                if (block.LiquidCode != "water" && place == true && depth >= 10 && depth <= 40)
                {
                    belowPos.Up();
                    for (int a = 0; a <= z; a++)
                    {
                        block = blockAccessor.GetBlock(belowPos);
                        if (block.LiquidCode == "water")
                        {
                            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:giantkelp-section")).BlockId, belowPos);
                            belowPos.Up();
                        }
                        else { belowPos.Down(); belowPos.Down(); break; }
                    }
                    blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:giantkelp-top")).BlockId, belowPos);
                    return true;
                }
                depth++;
            }
            return false;
        }
    }

    public class Sugarkelp : Block
    {
        int age = 0;
        private float tickGrowthProbability = 1f;

        public override bool TryPlaceBlockForWorldGen(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand)
        {
            base.TryPlaceBlockForWorldGen(blockAccessor, pos, onBlockFace, worldGenRand);

            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);

            int z = api.World.Rand.Next(3, 7);

            BlockPos belowPos = pos.Down();
            bool place = false;
            Block block = blockAccessor.GetBlock(belowPos);
            if (block.LiquidCode != "water") return false;

            int depth = 1;

            while (depth < 200)
            {
                belowPos.Down();
                block = blockAccessor.GetBlock(belowPos);
                if (block.LiquidCode == "water") { place = true; }
                if (block.LiquidCode != "water" && place == true)
                {
                    belowPos.Up();
                    blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:sugarkelp-buttom")).BlockId, belowPos);
                    belowPos.Up();
                    for (int a = 0; a <= z; a++)
                    {
                        block = blockAccessor.GetBlock(belowPos);
                        if (block.LiquidCode == "water")
                        {
                            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:sugarkelp-section")).BlockId, belowPos);
                            belowPos.Up();
                        }
                        else { belowPos.Down(); belowPos.Down(); break; }
                    }
                    blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:sugarkelp-top")).BlockId, belowPos);
                    return true;
                }
                depth++;
            }
            return false;
        }

        public override bool ShouldReceiveServerGameTicks(IWorldAccessor world, BlockPos pos, Random offThreadRandom, out object extra)
        {
            extra = null;

            if (Variant["type"] == "top")
            {
                extra = "omniummod:sugarkelp-section";
                return true;
            }
            return false;
        }

        public override void OnServerGameTick(IWorldAccessor world, BlockPos pos, object extra = null)
        {
            BlockPos setposition = new BlockPos(pos.X, pos.Y, pos.Z);
            Block block = extra as Block;
            //if (age <= 10)
            //{
            world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:sugarkelp-section")).Id, setposition);
            setposition.Up();
            if (world.BlockAccessor.GetBlock(setposition).Id == world.BlockAccessor.GetBlock(new AssetLocation("water-still-7")).Id)
            {
                //Block kelp = new Block();
                //((Sugarkelp)kelp).age = this.age + 1;
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:sugarkelp-top")).Id, setposition);
            }
        }

        public override void OnDecalTesselation(IWorldAccessor world, MeshData decalMesh, BlockPos pos)
        {
            base.OnDecalTesselation(world, decalMesh, pos);

            BlockPos setposition = new BlockPos(pos.X, pos.Y, pos.Z);

            world.BlockAccessor.SetBlock(1, setposition);
            setposition.Up();
            if (world.BlockAccessor.GetBlock(setposition).Id == world.BlockAccessor.GetBlock(new AssetLocation("water-still-7")).Id)
            {
                Block kelp = new Block();
                ((Sugarkelp)kelp).age = this.age + 1;
                world.BlockAccessor.SetBlock(kelp.Id, setposition);
            }
        }

    }

    public class Bullkelp : Block
    {
        public override bool TryPlaceBlockForWorldGen(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand)
        {
            base.TryPlaceBlockForWorldGen(blockAccessor, pos, onBlockFace, worldGenRand);

            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);

            int z = api.World.Rand.Next(3, 10);

            BlockPos belowPos = pos.Down();
            bool place = false;
            Block block = blockAccessor.GetBlock(belowPos);
            if (block.LiquidCode != "water") return false;

            int depth = 1;

            while (depth < 200)
            {
                belowPos.Down();
                block = blockAccessor.GetBlock(belowPos);
                if (block.LiquidCode == "water") { place = true; }
                if (block.LiquidCode != "water" && place == true && depth >= 10 && depth <= 40)
                {
                    belowPos.Up();
                    for (int a = 0; a <= z; a++)
                    {
                        block = blockAccessor.GetBlock(belowPos);
                        if (block.LiquidCode == "water")
                        {
                            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:bullkelp-section")).BlockId, belowPos);
                            belowPos.Up();
                        }
                        else { belowPos.Down(); belowPos.Down(); break; }
                    }
                    blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:bullkelp-top")).BlockId, belowPos);
                    return true;
                }
                depth++;
            }
            return false;
        }
    }

    public class Seagrass : Block
    {
        public override bool TryPlaceBlockForWorldGen(IBlockAccessor blockAccessor, BlockPos pos, BlockFacing onBlockFace, LCGRandom worldGenRand)
        {
            base.TryPlaceBlockForWorldGen(blockAccessor, pos, onBlockFace, worldGenRand);

            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);

            int z = api.World.Rand.Next(1, 3);

            BlockPos belowPos = pos.Down();
            bool place = false;
            Block block = blockAccessor.GetBlock(belowPos);
            if (block.LiquidCode != "water") return false;

            int depth = 1;

            while (depth < 200)
            {
                belowPos.Down();
                block = blockAccessor.GetBlock(belowPos);
                if (block.LiquidCode == "water") { place = true; }
                if (block.LiquidCode != "water" && place == true && depth >= 10 && depth <= 40)
                {
                    belowPos.Up();
                    for (int a = 0; a <= z; a++)
                    {
                        block = blockAccessor.GetBlock(belowPos);
                        if (block.LiquidCode == "water")
                        {
                            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:seagrass-normal")).BlockId, belowPos);
                            belowPos.Up();
                        }
                        else { belowPos.Down(); belowPos.Down(); break; }
                    }
                    blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:seagrass-top")).BlockId, belowPos);
                    return true;
                }
                depth++;
            }
            return false;
        }
    }


    public class Altar : Block
    {
        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            //return base.OnBlockInteractStart(world, byPlayer, blockSel);

            ItemStack stack = byPlayer.InventoryManager.ActiveHotbarSlot.Itemstack;
            BlockPos blockpos = new BlockPos(blockSel.Position.X, blockSel.Position.Y, blockSel.Position.Z);

            try
            {
            if (stack.Item.Id == world.GetItem(new AssetLocation("omniummod:eternitycrystal")).Id)
            {
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:altar-full")).BlockId, blockSel.Position);
                EntityProperties type = world.GetEntityType(new AssetLocation("omniummod:bosssummoner"));
                Entity ent = world.ClassRegistry.CreateEntity(type);
                ent.ServerPos.SetPos(new EntityPos((float)blockSel.Position.X + 0.5f, (float)blockSel.Position.Y + 2, (float)blockSel.Position.Z + 0.5f));
                ent.ServerPos.Motion.Set(0, 0, 0);
                ent.Pos.SetFrom(ent.ServerPos);
                blockpos.Down();
                if (world.BlockAccessor.GetBlock(blockpos).Id == world.BlockAccessor.GetBlock(new AssetLocation("mantle")).Id) ((Bosssummoner)ent).teleport = false;
                ((Bosssummoner)ent).spawnpos = blockSel.Position;
                world.SpawnEntity(ent);

                AssetLocation sound = new AssetLocation("omniummod", "sounds/entity/land/bosses/spawnsound");
                world.PlaySoundAt(sound, byPlayer.Entity, null, false, 100, 1);

                SimpleParticleProperties myParticles = new SimpleParticleProperties(30, 50, ColorUtil.ColorFromRgba(255, 255, 0, 0), new Vec3d(blockSel.Position.X - 1, blockSel.Position.Y, blockSel.Position.Z - 1),
                    new Vec3d(blockSel.Position.X + 2, blockSel.Position.Y + 2, blockSel.Position.Z + 2), new Vec3f(0, 0, 0), new Vec3f(0, -0.5f, 0), 500, 0, 2, 3);
                world.SpawnParticles(myParticles);

                myParticles = new SimpleParticleProperties(30, 50, ColorUtil.ColorFromRgba(255, 255, 0, 0), new Vec3d(blockSel.Position.X, blockSel.Position.Y, blockSel.Position.Z),
                    new Vec3d(blockSel.Position.X + 1, blockSel.Position.Y + 15, blockSel.Position.Z + 1), new Vec3f(-0.5f, -0.5f, -0.5f), new Vec3f(0.6f, -0.5f, 0.6f), 10, 0, 2, 3);
                world.SpawnParticles(myParticles);

                ItemSlot targetSlot = byPlayer.InventoryManager.ActiveHotbarSlot;
                targetSlot = new DummySlot(targetSlot.TakeOut(1));
                byPlayer.InventoryManager.ActiveHotbarSlot.MarkDirty();

                return true;
            }
            }
            catch { }
            return false;
        }
    }

    public class Dungeondoor : Block
    {
        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (Variant["state"] == "close") openUP(blockSel.Position, world, byPlayer);
            else if (Variant["state"] == "open") closeDOWN(blockSel.Position, world, byPlayer);
            return true;
        }

        public override AssetLocation GetRotatedBlockCode(int angle)
        {
            BlockFacing nowFacing = BlockFacing.FromCode(Variant["horizontalorientation"]);
            BlockFacing rotatedFacing = BlockFacing.HORIZONTALS_ANGLEORDER[(nowFacing.HorizontalAngleIndex + angle / 90) % 4];

            string type = Variant["horizontalorientation"];

            if (angle == 90)
            {
                if (type == "north") type = "east";
                else if (type == "east") type = "south";
                else if (type == "south") type = "west";
                else if (type == "west") type = "north";
            }
            if (angle == 180)
            {
                if (type == "north") type = "south";
                else if (type == "east") type = "west";
                else if (type == "south") type = "north";
                else if (type == "west") type = "east";
            }
            if (angle == 270)
            {
                if (type == "north") type = "west";
                else if (type == "east") type = "north";
                else if (type == "south") type = "east";
                else if (type == "west") type = "south";
            }



            /*if (nowFacing.Axis != rotatedFacing.Axis)
            {
                if (rotatedFacing.Code == "west") type = "east";
                if (rotatedFacing.Code == "south") type = "north";
            }*/

            return CodeWithVariant("horizontalorientation", type);
        }

        public void openUP(BlockPos pos, IWorldAccessor world, IPlayer byPlayer)
        {

            ItemStack stack = byPlayer.InventoryManager.ActiveHotbarSlot.Itemstack;

            try
            {
                if (stack.Item.Id == world.GetItem(new AssetLocation("omniummod:dungeonkey-one")).Id || stack.Item.Id == world.GetItem(new AssetLocation("omniummod:dungeonkey-many")).Id)
                {
                    AssetLocation sound = new AssetLocation("omniummod", "sounds/block/dungeondoor");
                    world.PlaySoundAt(sound, (double)pos.X, (double)pos.Y, (double)pos.Z, null, 1, 10, 1);

                    if (Variant["horizontalorientation"] == "north")
                    {
                        world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-north")).Id, pos);

                        int id = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-open-north")).Id;
                        for (int a = 0; a <= 1; a++)
                        {
                            pos.Up();
                            world.BlockAccessor.SetBlock(id, pos);
                            pos.Down();
                            pos.East();
                            world.BlockAccessor.SetBlock(id, pos);
                        }
                        pos.Up();
                        world.BlockAccessor.SetBlock(id, pos);
                    }
                    if (Variant["horizontalorientation"] == "south")
                    {
                        world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-south")).Id, pos);

                        int id = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-open-south")).Id;
                        for (int a = 0; a <= 1; a++)
                        {
                            pos.Up();
                            world.BlockAccessor.SetBlock(id, pos);
                            pos.Down();
                            pos.West();
                            world.BlockAccessor.SetBlock(id, pos);
                        }
                        pos.Up();
                        world.BlockAccessor.SetBlock(id, pos);
                    }
                    if (Variant["horizontalorientation"] == "east")
                    {
                        world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-east")).Id, pos);

                        int id = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-open-east")).Id;
                        for (int a = 0; a <= 1; a++)
                        {
                            pos.Up();
                            world.BlockAccessor.SetBlock(id, pos);
                            pos.Down();
                            pos.South();
                            world.BlockAccessor.SetBlock(id, pos);
                        }
                        pos.Up();
                        world.BlockAccessor.SetBlock(id, pos);
                    }
                    if (Variant["horizontalorientation"] == "west")
                    {
                        world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-west")).Id, pos);

                        int id = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-open-west")).Id;
                        for (int a = 0; a <= 1; a++)
                        {
                            pos.Up();
                            world.BlockAccessor.SetBlock(id, pos);
                            pos.Down();
                            pos.North();
                            world.BlockAccessor.SetBlock(id, pos);
                        }
                        pos.Up();
                        world.BlockAccessor.SetBlock(id, pos);
                    }

                    if (stack.Item.Id == world.GetItem(new AssetLocation("omniummod:dungeonkey-one")).Id)
                    {
                        ItemSlot targetSlot = byPlayer.InventoryManager.ActiveHotbarSlot;
                        targetSlot = new DummySlot(targetSlot.TakeOut(1));
                        byPlayer.InventoryManager.ActiveHotbarSlot.MarkDirty();
                    }
                }
            }
            catch { }
        }

        public void closeDOWN(BlockPos pos, IWorldAccessor world, IPlayer byPlayer)
        {
            ItemStack stack = byPlayer.InventoryManager.ActiveHotbarSlot.Itemstack;

            try
            {
                if (stack.Item.Id == world.GetItem(new AssetLocation("omniummod:dungeonkey-one")).Id || stack.Item.Id == world.GetItem(new AssetLocation("omniummod:dungeonkey-many")).Id)
                {
                    AssetLocation sound = new AssetLocation("omniummod", "sounds/block/dungeondoor");
                    world.PlaySoundAt(sound, (double)pos.X, (double)pos.Y, (double)pos.Z, null, 1, 10, 1);

                    if (Variant["horizontalorientation"] == "north")
                    {
                        world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-north")).Id, pos);

                        int id = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-close-north")).Id;
                        for (int a = 0; a <= 1; a++)
                        {
                            pos.Up();
                            world.BlockAccessor.SetBlock(id, pos);
                            pos.Down();
                            pos.East();
                            world.BlockAccessor.SetBlock(id, pos);
                        }
                        pos.Up();
                        world.BlockAccessor.SetBlock(id, pos);
                    }
                    if (Variant["horizontalorientation"] == "south")
                    {
                        world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-south")).Id, pos);

                        int id = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-close-south")).Id;
                        for (int a = 0; a <= 1; a++)
                        {
                            pos.Up();
                            world.BlockAccessor.SetBlock(id, pos);
                            pos.Down();
                            pos.West();
                            world.BlockAccessor.SetBlock(id, pos);
                        }
                        pos.Up();
                        world.BlockAccessor.SetBlock(id, pos);
                    }
                    if (Variant["horizontalorientation"] == "east")
                    {
                        world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-east")).Id, pos);

                        int id = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-close-east")).Id;
                        for (int a = 0; a <= 1; a++)
                        {
                            pos.Up();
                            world.BlockAccessor.SetBlock(id, pos);
                            pos.Down();
                            pos.South();
                            world.BlockAccessor.SetBlock(id, pos);
                        }
                        pos.Up();
                        world.BlockAccessor.SetBlock(id, pos);
                    }
                    if (Variant["horizontalorientation"] == "west")
                    {
                        world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-west")).Id, pos);

                        int id = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-close-west")).Id;
                        for (int a = 0; a <= 1; a++)
                        {
                            pos.Up();
                            world.BlockAccessor.SetBlock(id, pos);
                            pos.Down();
                            pos.North();
                            world.BlockAccessor.SetBlock(id, pos);
                        }
                        pos.Up();
                        world.BlockAccessor.SetBlock(id, pos);
                    }

                    if (stack.Item.Id == world.GetItem(new AssetLocation("omniummod:dungeonkey-one")).Id)
                    {
                        ItemSlot targetSlot = byPlayer.InventoryManager.ActiveHotbarSlot;
                        targetSlot = new DummySlot(targetSlot.TakeOut(1));
                        byPlayer.InventoryManager.ActiveHotbarSlot.MarkDirty();
                    }
                }
            }
            catch { }
        }

        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            IBlockAccessor blockAccessor = world.BlockAccessor;
            //blockAccessor.SetBlock(2, blockSel.Position);
            if (blockSel == null || blockSel.Position == (BlockPos)null || blockSel.Face != BlockFacing.UP)
                return false;
            BlockFacing facing = Block.SuggestedHVOrientation(byPlayer, blockSel)[0];
            //blockAccessor.SetBlock(3, blockSel.Position);
            AssetLocation code = this.CodeWithVariants(new Dictionary<string, string>()
            {
                { "horizontalorientation", facing.Code },
                { "state", "close" }
            });
            Block block = blockAccessor.GetBlock(code);
            //blockAccessor.SetBlock(4, blockSel.Position);
            return setDoor(blockSel.Position, world, block);
        }

        public override void OnBlockRemoved(IWorldAccessor world, BlockPos pos1)
        {
            int id = world.BlockAccessor.GetBlock(new AssetLocation("air")).Id;
            BlockPos pos = new BlockPos(pos1.X, pos1.Y, pos1.Z);

            if (Variant["horizontalorientation"] == "north")
            {
                for (int a = 0; a <= 1; a++)
                {
                    pos.Up();
                    world.BlockAccessor.SetBlock(id, pos);
                    pos.Down();
                    pos.East();
                    world.BlockAccessor.SetBlock(id, pos);
                }
                pos.Up();
                world.BlockAccessor.SetBlock(id, pos);
            }
            if (Variant["horizontalorientation"] == "south")
            {
                for (int a = 0; a <= 1; a++)
                {
                    pos.Up();
                    world.BlockAccessor.SetBlock(id, pos);
                    pos.Down();
                    pos.West();
                    world.BlockAccessor.SetBlock(id, pos);
                }
                pos.Up();
                world.BlockAccessor.SetBlock(id, pos);
            }
            if (Variant["horizontalorientation"] == "east")
            {
                for (int a = 0; a <= 1; a++)
                {
                    pos.Up();
                    world.BlockAccessor.SetBlock(id, pos);
                    pos.Down();
                    pos.South();
                    world.BlockAccessor.SetBlock(id, pos);
                }
                pos.Up();
                world.BlockAccessor.SetBlock(id, pos);
            }
            if (Variant["horizontalorientation"] == "west")
            {
                for (int a = 0; a <= 1; a++)
                {
                    pos.Up();
                    world.BlockAccessor.SetBlock(id, pos);
                    pos.Down();
                    pos.North();
                    world.BlockAccessor.SetBlock(id, pos);
                }
                pos.Up();
                world.BlockAccessor.SetBlock(id, pos);
            }
        }


        public bool setDoor(BlockPos pos, IWorldAccessor world, Block block)
        {
            IBlockAccessor blockAccessor = world.BlockAccessor;

            if (block.Variant["horizontalorientation"] == "north")
            {
                int id = blockAccessor.GetBlock(new AssetLocation("air")).Id;
                BlockPos pos2 = pos;
                for (int a = 0; a <= 2; a++)
                {

                    if (blockAccessor.GetBlock(pos2).Id != id && blockAccessor.GetBlock(pos2).LiquidCode != "water") return false;
                    pos2.Up();

                    if (blockAccessor.GetBlock(pos2).Id != id && blockAccessor.GetBlock(pos2).LiquidCode != "water") return false;
                    pos2.Down();
                    pos2.East();
                }

                pos.West(); pos.West(); pos.West();

                blockAccessor.SetBlock(block.Id, pos);

                id = blockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-close-north")).Id;
                for (int a = 0; a <= 1; a++)
                {
                    pos.Up();
                    blockAccessor.SetBlock(id, pos);
                    pos.Down();
                    pos.East();
                    blockAccessor.SetBlock(id, pos);
                }
                pos.Up();
                blockAccessor.SetBlock(id, pos);
                pos.Down(); pos.West(); pos.West();
                return true;

            }
            if (block.Variant["horizontalorientation"] == "south")
            {
                int id = blockAccessor.GetBlock(new AssetLocation("air")).Id;
                BlockPos pos2 = pos;
                for (int a = 0; a <= 2; a++)
                {

                    if (blockAccessor.GetBlock(pos2).Id != id && blockAccessor.GetBlock(pos2).LiquidCode != "water") return false;
                    pos2.Up();

                    if (blockAccessor.GetBlock(pos2).Id != id && blockAccessor.GetBlock(pos2).LiquidCode != "water") return false;
                    pos2.Down();
                    pos2.West();
                }

                pos.East(); pos.East(); pos.East();

                blockAccessor.SetBlock(block.Id, pos);

                id = blockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-close-south")).Id;
                for (int a = 0; a <= 1; a++)
                {
                    pos.Up();
                    blockAccessor.SetBlock(id, pos);
                    pos.Down();
                    pos.West();
                    blockAccessor.SetBlock(id, pos);
                }
                pos.Up();
                blockAccessor.SetBlock(id, pos);
                pos.Down(); pos.East(); pos.East();
                return true;

            }
            else if (block.Variant["horizontalorientation"] == "east")
            {
                int id = blockAccessor.GetBlock(new AssetLocation("air")).Id;
                BlockPos pos2 = pos;
                for (int a = 0; a <= 2; a++)
                {
                    if (blockAccessor.GetBlock(pos2).Id != id && blockAccessor.GetBlock(pos2).LiquidCode != "water") return false;
                    pos2.Up();
                    if (blockAccessor.GetBlock(pos2).Id != id && blockAccessor.GetBlock(pos2).LiquidCode != "water") return false;
                    pos2.Down();
                    pos2.South();
                }
                pos.North(); pos.North(); pos.North();
                blockAccessor.SetBlock(block.Id, pos);
                id = blockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-close-east")).Id;
                for (int a = 0; a <= 1; a++)
                {
                    pos.Up();
                    blockAccessor.SetBlock(id, pos);
                    pos.Down();
                    pos.South();
                    blockAccessor.SetBlock(id, pos);
                }
                pos.Up();
                blockAccessor.SetBlock(id, pos);
                pos.Down(); pos.North(); pos.North();
                return true;
            }
            else if (block.Variant["horizontalorientation"] == "west")
            {
                int id = blockAccessor.GetBlock(new AssetLocation("air")).Id;
                BlockPos pos2 = pos;
                for (int a = 0; a <= 2; a++)
                {
                    if (blockAccessor.GetBlock(pos2).Id != id && blockAccessor.GetBlock(pos2).LiquidCode != "water") return false;
                    pos2.Up();
                    if (blockAccessor.GetBlock(pos2).Id != id && blockAccessor.GetBlock(pos2).LiquidCode != "water") return false;
                    pos2.Down();
                    pos2.North();
                }
                pos.South(); pos.South(); pos.South();
                blockAccessor.SetBlock(block.Id, pos);
                id = blockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor_ph-close-west")).Id;
                for (int a = 0; a <= 1; a++)
                {
                    pos.Up();
                    blockAccessor.SetBlock(id, pos);
                    pos.Down();
                    pos.North();
                    blockAccessor.SetBlock(id, pos);
                }
                pos.Up();
                blockAccessor.SetBlock(id, pos);
                pos.Down(); pos.South(); pos.South();
                return true;
            }

            return true;
        }
    }

    public class Dungeondoor_PH : Block
    {
        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            //return base.OnBlockInteractStart(world, byPlayer, blockSel);

            if (Variant["horizontalorientation"] == "north")
            {
                BlockPos pos = blockSel.Position;

                for (int a = 0; a <= 2; a++)
                {
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-north")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.openUP(pos, world, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-north")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.closeDOWN(pos, world, byPlayer);
                        break;
                    }
                    pos.Down();
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-north")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.openUP(pos, world, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-north")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.closeDOWN(pos, world, byPlayer);
                        break;
                    }
                    pos.Up();
                    pos.West();
                }
            }
            if (Variant["horizontalorientation"] == "south")
            {
                BlockPos pos = blockSel.Position;

                for (int a = 0; a <= 2; a++)
                {
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-south")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.openUP(pos, world, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-south")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.closeDOWN(pos, world, byPlayer);
                        break;
                    }
                    pos.Down();
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-south")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.openUP(pos, world, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-south")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.closeDOWN(pos, world, byPlayer);
                        break;
                    }
                    pos.Up();
                    pos.East();
                }
            }
            if (Variant["horizontalorientation"] == "east")
            {
                BlockPos pos = blockSel.Position;

                for (int a = 0; a <= 2; a++)
                {
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-east")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.openUP(pos, world, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-east")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.closeDOWN(pos, world, byPlayer);
                        break;
                    }
                    pos.Down();
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-east")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.openUP(pos, world, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-east")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.closeDOWN(pos, world, byPlayer);
                        break;
                    }
                    pos.Up();
                    pos.North();
                }
            }
            if (Variant["horizontalorientation"] == "west")
            {
                BlockPos pos = blockSel.Position;

                for (int a = 0; a <= 2; a++)
                {
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-west")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.openUP(pos, world, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-west")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.closeDOWN(pos, world, byPlayer);
                        break;
                    }
                    pos.Down();
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-west")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.openUP(pos, world, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-west")).Id)
                    {
                        Dungeondoor b = (Dungeondoor)world.BlockAccessor.GetBlock(pos);
                        b.closeDOWN(pos, world, byPlayer);
                        break;
                    }
                    pos.Up();
                    pos.South();
                }
            }


            return true;
        }

        public override void OnBlockBroken(IWorldAccessor world, BlockPos pos1, IPlayer byPlayer, float dropQuantityMultiplier = 1)
        {
            //if (world.BlockAccessor.GetBlock(pos1).Id == world.BlockAccessor.GetBlock(new AssetLocation("air")).Id)
            //{
            if (Variant["horizontalorientation"] == "north")
            {
                BlockPos pos = new BlockPos(pos1.X, pos1.Y, pos1.Z);

                for (int a = 0; a <= 2; a++)
                {
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-north")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-north")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    pos.Down();
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-north")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-north")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    pos.Up();
                    pos.West();
                }
            }
            if (Variant["horizontalorientation"] == "south")
            {
                BlockPos pos = new BlockPos(pos1.X, pos1.Y, pos1.Z);

                for (int a = 0; a <= 2; a++)
                {
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-south")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-south")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    pos.Down();
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-south")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-south")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    pos.Up();
                    pos.East();
                }
            }
            if (Variant["horizontalorientation"] == "east")
            {
                BlockPos pos = new BlockPos(pos1.X, pos1.Y, pos1.Z);

                for (int a = 0; a <= 2; a++)
                {
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-east")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-east")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    pos.Down();
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-east")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-east")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    pos.Up();
                    pos.North();
                }
            }
            if (Variant["horizontalorientation"] == "west")
            {
                BlockPos pos = new BlockPos(pos1.X, pos1.Y, pos1.Z);

                for (int a = 0; a <= 2; a++)
                {
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-west")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-west")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    pos.Down();
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-close-west")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    if (world.BlockAccessor.GetBlock(pos).Id == world.BlockAccessor.GetBlock(new AssetLocation("omniummod:dungeondoor-open-west")).Id)
                    {
                        world.BlockAccessor.BreakBlock(pos, byPlayer);
                        break;
                    }
                    pos.Up();
                    pos.South();
                }
            }
            //}
        }

        public override AssetLocation GetRotatedBlockCode(int angle)
        {
            BlockFacing nowFacing = BlockFacing.FromCode(Variant["horizontalorientation"]);
            BlockFacing rotatedFacing = BlockFacing.HORIZONTALS_ANGLEORDER[(nowFacing.HorizontalAngleIndex + angle / 90) % 4];

            string type = Variant["horizontalorientation"];

            if (angle == 90) 
            {
                if (type == "north") type = "east";
                else if (type == "east") type = "south";
                else if (type == "south") type = "west";
                else if (type == "west") type = "north";
            }
            if (angle == 180)
            {
                if (type == "north") type = "south";
                else if (type == "east") type = "west";
                else if (type == "south") type = "north";
                else if (type == "west") type = "east";
            }
            if (angle == 270)
            {
                if (type == "north") type = "west";
                else if (type == "east") type = "north";
                else if (type == "south") type = "east";
                else if (type == "west") type = "south";
            }



            /*if (nowFacing.Axis != rotatedFacing.Axis)
            {
                if (rotatedFacing.Code == "west") type = "east";
                if (rotatedFacing.Code == "south") type = "north";
            }*/

            return CodeWithVariant("horizontalorientation", type);
        }

    }

    public class DungeondoorEntity : BlockEntity
    {
        /*public void animate(BlockPos pos)
        {
            Vintagestory.GameContent.BlockEntityAnimationUtil anim = new Vintagestory.GameContent.BlockEntityAnimationUtil(this.Api, this);
            var meta = new AnimationMetaData() { Animation = "open", Code = "open", AnimationSpeed = 1, EaseInSpeed = 1, EaseOutSpeed = 2, Weight = 1, BlendMode = EnumAnimationBlendMode.Add };
            anim.StartAnimation(meta);
        }*/
    }

    public class Firetrap : Block
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            return SetTrap(world, blockSel.Position);
        }

        public bool SetTrap(IWorldAccessor world, BlockPos pos)
        {
            BlockPos posPh = new BlockPos(pos.X, pos.Y, pos.Z);
            BlockPos posTr = new BlockPos(pos.X, pos.Y, pos.Z);
            int AirId = world.BlockAccessor.GetBlock(new AssetLocation("air")).Id;

            if (world.BlockAccessor.GetBlock(posTr).Id != AirId && world.BlockAccessor.GetBlock(posTr).LiquidCode != "water") return false;
            world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:firetrap")).Id, posTr);

            SetPHs(world, new BlockPos(posTr.X, posTr.Y, posTr.Z));

            return true;
        }

        public void SetPHs(IWorldAccessor world, BlockPos pos)
        {
            BlockPos posPh = new BlockPos(pos.X, pos.Y, pos.Z);
            BlockPos posTr = new BlockPos(pos.X, pos.Y, pos.Z);
            int AirId = world.BlockAccessor.GetBlock(new AssetLocation("air")).Id;

            for (int a = 0; a <= 3; a++)
            {
                posPh.West();
                if (world.BlockAccessor.GetBlock(posPh).Id == AirId)
                {
                    world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:firetrap_ph")).Id, posPh);
                }
                else break;
            }
            posPh = new BlockPos(posTr.X, posTr.Y, posTr.Z);

            for (int a = 0; a <= 3; a++)
            {
                posPh.North();
                if (world.BlockAccessor.GetBlock(posPh).Id == AirId)
                {
                    world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:firetrap_ph")).Id, posPh);
                }
                else break;
            }
            posPh = new BlockPos(posTr.X, posTr.Y, posTr.Z);

            for (int a = 0; a <= 3; a++)
            {
                posPh.East();
                if (world.BlockAccessor.GetBlock(posPh).Id == AirId)
                {
                    world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:firetrap_ph")).Id, posPh);
                }
                else break;
            }
            posPh = new BlockPos(posTr.X, posTr.Y, posTr.Z);

            for (int a = 0; a <= 3; a++)
            {
                posPh.South();
                if (world.BlockAccessor.GetBlock(posPh).Id == AirId)
                {
                    world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:firetrap_ph")).Id, posPh);
                }
                else break;
            }
            posPh = new BlockPos(posTr.X, posTr.Y, posTr.Z);
        }

        public void RemovePHs(IWorldAccessor world, BlockPos pos)
        {
            BlockPos posPh = new BlockPos(pos.X, pos.Y, pos.Z);
            BlockPos posTr = new BlockPos(pos.X, pos.Y, pos.Z);
            int AirId = world.BlockAccessor.GetBlock(new AssetLocation("air")).Id;
            int PHId = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:firetrap_ph")).Id;

            for (int a = 0; a <= 3; a++)
            {
                posPh.West();
                if (world.BlockAccessor.GetBlock(posPh).Id == PHId)
                {
                    world.BlockAccessor.SetBlock(AirId, posPh);
                }
            }
            posPh = new BlockPos(posTr.X, posTr.Y, posTr.Z);

            for (int a = 0; a <= 3; a++)
            {
                posPh.North();
                if (world.BlockAccessor.GetBlock(posPh).Id == PHId)
                {
                    world.BlockAccessor.SetBlock(AirId, posPh);
                }
            }
            posPh = new BlockPos(posTr.X, posTr.Y, posTr.Z);

            for (int a = 0; a <= 3; a++)
            {
                posPh.East();
                if (world.BlockAccessor.GetBlock(posPh).Id == PHId)
                {
                    world.BlockAccessor.SetBlock(AirId, posPh);
                }
            }
            posPh = new BlockPos(posTr.X, posTr.Y, posTr.Z);

            for (int a = 0; a <= 3; a++)
            {
                posPh.South();
                if (world.BlockAccessor.GetBlock(posPh).Id == PHId)
                {
                    world.BlockAccessor.SetBlock(AirId, posPh);
                }
            }
            posPh = new BlockPos(posTr.X, posTr.Y, posTr.Z);
        }

        public override void OnBlockRemoved(IWorldAccessor world, BlockPos pos)
        {
            RemovePHs(world, pos);
        }
    }

    public class Firetrap_PH : Block
    {
        int z = 20;

        public override void OnEntityInside(IWorldAccessor world, Entity entity, BlockPos pos)
        {
            try
            {
                z += 1;
                if (entity.Class != "EntityItem" && entity.Alive == true)
                {
                    BlockPos TrapPos = GetTrapPos(world, pos);
                    if (TrapPos != null)
                    {
                        SimpleParticleProperties myParticles = new SimpleParticleProperties(2, 5, ColorUtil.ColorFromRgba(world.Rand.Next(30, 50), world.Rand.Next(100, 150), world.Rand.Next(200, 255), world.Rand.Next(100, 200)), entity.Pos.XYZ, new Vec3d(TrapPos.X + 0.5, TrapPos.Y + 0.5, TrapPos.Z + 0.5), new Vec3f(-0.1f, -0.1f, -0.1f), new Vec3f(0.1f, 0.1f, 0.1f), 1f, 0, 0.5f, 1, EnumParticleModel.Quad);
                        myParticles.UseLighting();
                        world.SpawnParticles(myParticles);
                    }
                    if (z >= 20)
                    {
                        AssetLocation sound = new AssetLocation("omniummod", "sounds/block/firetrap");
                        world.PlaySoundAt(sound, (double)pos.X, (double)pos.Y, (double)pos.Z, null, 1, 32, 1);
                        z = 0;
                    }
                    entity.Ignite();
                }
            }
            catch { }
        }

        /*public override void OnNeighbourBlockChange(IWorldAccessor world, BlockPos pos, BlockPos neibpos)
        {
            if (world.BlockAccessor.GetBlock(neibpos).Id == world.BlockAccessor.GetBlock(new AssetLocation("air")).Id)
            {
                BlockPos posTr = GetTrapPos(world, pos);
                Firetrap TrapBlock = (Firetrap)world.BlockAccessor.GetBlock(posTr);
                TrapBlock.RemovePHs(world, new BlockPos(posTr.X, posTr.Y, posTr.Z));
                TrapBlock.SetPHs(world, new BlockPos(posTr.X, posTr.Y, posTr.Z));
            }
        }*/

        public BlockPos GetTrapPos(IWorldAccessor world, BlockPos pos)
        {
            BlockPos posPh = new BlockPos(pos.X, pos.Y, pos.Z);
            BlockPos posTr = new BlockPos(pos.X, pos.Y, pos.Z);
            int AirId = world.BlockAccessor.GetBlock(new AssetLocation("air")).Id;
            int TrapId = world.BlockAccessor.GetBlock(new AssetLocation("omniummod:firetrap")).Id;

            for (int a = 0; a <= 3; a++)
            {
                posTr.West();
                if (world.BlockAccessor.GetBlock(posTr).Id == TrapId)
                {
                    return posTr;
                }
            }
            posTr = new BlockPos(posPh.X, posPh.Y, posPh.Z);
            for (int a = 0; a <= 3; a++)
            {
                posTr.East();
                if (world.BlockAccessor.GetBlock(posTr).Id == TrapId)
                {
                    return posTr;
                }
            }
            posTr = new BlockPos(posPh.X, posPh.Y, posPh.Z);
            for (int a = 0; a <= 3; a++)
            {
                posTr.South();
                if (world.BlockAccessor.GetBlock(posTr).Id == TrapId)
                {
                    return posTr;
                }
            }
            posTr = new BlockPos(posPh.X, posPh.Y, posPh.Z);
            for (int a = 0; a <= 3; a++)
            {
                posTr.North();
                if (world.BlockAccessor.GetBlock(posTr).Id == TrapId)
                {
                    return posTr;
                }
            }
            posTr = new BlockPos(posPh.X, posPh.Y, posPh.Z);

            return null;
        }
    }

    public class Spikes : Block
    {
        public override void OnEntityCollide(IWorldAccessor world, Entity entity, BlockPos pos, BlockFacing facing, Vec3d collideSpeed, bool isImpact)
        {
            entity.ReceiveDamage(new DamageSource() { SourceBlock = this }, 0.5f);
        }
    }

    public class Soulgolem_Block : Block
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            IBlockAccessor blockAccessor = world.BlockAccessor;
            if (blockSel == null || blockSel.Position == (BlockPos)null || blockSel.Face != BlockFacing.UP)
                return false;
            BlockFacing facing = Block.SuggestedHVOrientation(byPlayer, blockSel)[0];
            AssetLocation code = this.CodeWithVariants(new Dictionary<string, string>()
            {
                { "horizontalorientation", facing.Code },
                { "state", "buttom" }
            });
            Block block = blockAccessor.GetBlock(code);
            blockAccessor.SetBlock(block.Id, blockSel.Position);
            BlockPos pos = new BlockPos(blockSel.Position.X, blockSel.Position.Y, blockSel.Position.Z);
            pos.Up();
            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:soulgolem-middle-" + block.Variant["horizontalorientation"])).Id, pos);
            pos.Up();
            blockAccessor.SetBlock(blockAccessor.GetBlock(new AssetLocation("omniummod:soulgolem-top-" + block.Variant["horizontalorientation"])).Id, pos);
            return true;
        }

        public override void OnBlockBroken(IWorldAccessor world, BlockPos pos1, IPlayer byPlayer, float dropQuantityMultiplier = 1)
        {
            Block block = world.BlockAccessor.GetBlock(pos1);
            BlockPos pos = new BlockPos(pos1.X, pos1.Y, pos1.Z);
            BlockPos pos2 = new BlockPos(pos1.X, pos1.Y, pos1.Z);

            if (block.Variant["state"] == "buttom")
            {
                pos2 = new BlockPos(pos.X, pos.Y, pos.Z);
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
                pos.Up();
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
                pos.Up();
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
            }
            if (block.Variant["state"] == "middle")
            {
                pos.Down();
                pos2 = new BlockPos(pos.X, pos.Y, pos.Z);
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
                pos.Up();
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
                pos.Up();
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
            }
            if (block.Variant["state"] == "top")
            {
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
                pos.Down();
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
                pos.Down();
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).BlockId, pos);
                pos2 = new BlockPos(pos.X, pos.Y, pos.Z);
            }

            EntityProperties type = world.GetEntityType(new AssetLocation("omniummod:soulgolemmonster"));
            Entity ent = world.ClassRegistry.CreateEntity(type);
            ent.ServerPos.SetPos(new EntityPos((float)pos2.X + 0.5f, (float)pos2.Y, (float)pos2.Z + 0.5f));
            ent.ServerPos.Motion.Set(0, 0, 0);
            ent.Pos.SetFrom(ent.ServerPos);
            world.SpawnEntity(ent);
        }
    }

    public class Iceblock : Block
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            BlockPos pos = new BlockPos(blockSel.Position.X, blockSel.Position.Y, blockSel.Position.Z);
            pos.Up();
            if (!(world.BlockAccessor.GetBlock(pos).Id == 0 || world.BlockAccessor.GetBlock(pos).LiquidCode == "water")) return false;
            pos.Down();
            world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:iceblock-buttom")).Id, pos);
            pos.Up();
            world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("omniummod:iceblock-top")).Id, pos);
            return true;
        }

        public override void OnBlockBroken(IWorldAccessor world, BlockPos pos1, IPlayer byPlayer, float dropQuantityMultiplier = 1)
        {
            Block block = world.BlockAccessor.GetBlock(pos1);
            BlockPos pos = new BlockPos(pos1.X, pos1.Y, pos1.Z);


            if (block.Variant["type"] == "buttom")
            {
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).Id, pos);
                pos.Up();
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).Id, pos);
            }
            if (block.Variant["type"] == "top")
            {
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).Id, pos);
                pos.Down();
                world.BlockAccessor.SetBlock(world.BlockAccessor.GetBlock(new AssetLocation("air")).Id, pos);
            }
        }
    }

    public class Fishbucket : Block
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {


            if (byPlayer.Entity.Controls.Sneak)
            {

                if (byPlayer.Entity is EntityPlayer)
                {
                    ItemStack stack = ((EntityPlayer)byPlayer.Entity).Player.InventoryManager.ActiveHotbarSlot.Itemstack;
                    Entity entity = null;
                    EntityProperties type = null;

                    if (Variant["type"] == "blobfisch")
                    {
                        type = byPlayer.Entity.World.GetEntityType(new AssetLocation("omniummod:blobfischnd"));
                    }
                    if (Variant["type"] == "geisterfisch")
                    {
                        type = byPlayer.Entity.World.GetEntityType(new AssetLocation("omniummod:geisterfischnd"));
                    }
                    if (Variant["type"] == "hecht")
                    {
                        type = byPlayer.Entity.World.GetEntityType(new AssetLocation("omniummod:hechtnd"));
                    }
                    if (Variant["type"] == "lachs")
                    {
                        type = byPlayer.Entity.World.GetEntityType(new AssetLocation("omniummod:lachsnd"));
                    }
                    if (Variant["type"] == "höhlenfisch")
                    {
                        type = byPlayer.Entity.World.GetEntityType(new AssetLocation("omniummod:höhlenfischnd"));
                    }
                    if (Variant["type"] == "butterflifish")
                    {
                        type = byPlayer.Entity.World.GetEntityType(new AssetLocation("omniummod:butterflifishnd"));
                    }

                    if (type != null)
                    {
                        ItemSlot targetSlot = byPlayer.InventoryManager.ActiveHotbarSlot;
                        targetSlot = new DummySlot(targetSlot.TakeOut(1));
                        byPlayer.InventoryManager.ActiveHotbarSlot.MarkDirty();

                        byPlayer.InventoryManager.TryGiveItemstack(new ItemStack(byPlayer.Entity.World.GetBlock(new AssetLocation("omniummod:fischbucket-empty")), 1), true);

                        entity = byPlayer.Entity.World.ClassRegistry.CreateEntity(type);
                        EntityPos pos = new EntityPos((double)blockSel.Position.X + 0.5, (double)blockSel.Position.Y, (double)blockSel.Position.Z + 0.5, 0, 0, 0);

                        entity.ServerPos.SetPos(pos);
                        entity.ServerPos.Motion.Set(0, 0, 0);

                        entity.Pos.SetFrom(entity.ServerPos);
                        entity.World = byPlayer.Entity.World;
                        
                        byPlayer.Entity.World.BlockAccessor.SetBlock(byPlayer.Entity.World.BlockAccessor.GetBlock(new AssetLocation("game:water-still-7")).Id, blockSel.Position);
                        byPlayer.Entity.World.BlockAccessor.TriggerNeighbourBlockUpdate(new BlockPos(blockSel.Position.X, blockSel.Position.Y + 1, blockSel.Position.Z));


                        byPlayer.Entity.World.SpawnEntity(entity);
                        
                        return true;
                    }
                }


            }
            return base.TryPlaceBlock(world, byPlayer, itemstack, blockSel, ref failureCode);
        }
    }


    //--------------Items and dependincies to them

    public class Scourgethrower : Item
    {
        public override void OnHeldAttackStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, ref EnumHandHandling handling)
        {
            float damage = 10;

            IPlayer byPlayer = null;
            if (byEntity is EntityPlayer) byPlayer = byEntity.World.PlayerByUid(((EntityPlayer)byEntity).PlayerUID);
            byEntity.World.PlaySoundAt(new AssetLocation("omniummod:sounds/item/scourgethrower"), byEntity, byPlayer, false, 8);

            EntityProperties type = byEntity.World.GetEntityType(new AssetLocation("omniummod:scourgethrown"));//scourgethrown
            Entity entity = byEntity.World.ClassRegistry.CreateEntity(type);
            ((Scourgethrown)entity).FiredBy = byEntity;
            ((Scourgethrown)entity).Damage = damage;


            Vec3d pos = byEntity.ServerPos.XYZ.Add(0, byEntity.LocalEyePos.Y, 0);
            Vec3d aheadPos = pos.AheadCopy(1.5f, byEntity.SidedPos.Pitch, byEntity.SidedPos.Yaw);
            Vec3d velocity = (aheadPos - pos) * 0.3;


            entity.ServerPos.SetPos(byEntity.SidedPos.AheadCopy(1.5f).XYZ.Add(0, byEntity.LocalEyePos.Y, 0));
            entity.ServerPos.Motion.Set(velocity);



            entity.Pos.SetFrom(entity.ServerPos);
            entity.World = byEntity.World;
            ((Scourgethrown)entity).SetRotation();

            byEntity.World.SpawnEntity(entity);

            //slot.Itemstack.Collectible.DamageItem(byEntity.World, byEntity, slot);
            handling = EnumHandHandling.Handled;
        }
    }

    public class Scourgethrown : EntityAgent
    {
        public Entity FiredBy;
        public float Damage;
        double my = 0;
        int livelength = 0;

        public override void OnCollided()
        {
            this.Die(EnumDespawnReason.Removed);
        }

        public override void Initialize(EntityProperties properties, ICoreAPI api, long InChunkIndex3d)
        {
            base.Initialize(properties, api, InChunkIndex3d);
            my = this.ServerPos.Motion.Y;
        }

        public virtual void SetRotation()
        {
            EntityPos pos = (World is IServerWorldAccessor) ? ServerPos : Pos;

            double speed = pos.Motion.Length();

            if (speed > 0.01)
            {
                pos.Pitch = 0;
                pos.Yaw =
                    (GameMath.PI + (float)Math.Atan2(pos.Motion.X / speed, pos.Motion.Z / speed))
                ;
                pos.Roll =
                    (-(float)Math.Asin(GameMath.Clamp(-pos.Motion.Y / speed, -1, 1)))
                ;
            }
        }

        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);

            this.Pos.Motion.Y = my;

            livelength += 1;
            if (livelength >= 200) this.Die(EnumDespawnReason.Removed);

            SimpleParticleProperties myParticles = new SimpleParticleProperties(1, 1, ColorUtil.ColorFromRgba(255, 255, 0, 0), this.Pos.XYZ.AddCopy(-0.1, -0.1, -0.1), this.Pos.XYZ.AddCopy(0.1, -0.1, 0.1), new Vec3f(-0.1f, -0.1f, -0.1f), new Vec3f(0.1f, 0.1f, 0.1f), 1, 0, 0.5f, 1f);
            World.SpawnParticles(myParticles);
            Entity[] e = World.GetEntitiesAround(ServerPos.XYZ, 1f, 1f);

            if (Alive == true)
            {
                foreach (Entity ent in e)
                {
                    if (!(ent is Scourgethrown) && ent != FiredBy)
                    {
                        SimpleParticleProperties myParticles2 = new SimpleParticleProperties(5, 20, ColorUtil.ColorFromRgba(255, 255, 0, 0), this.Pos.XYZ.AddCopy(-0.1, -0.1, -0.1), this.Pos.XYZ.AddCopy(0.1, -0.1, 0.1), new Vec3f(-0.1f, -0.1f, -0.1f), new Vec3f(0.1f, 0.1f, 0.1f), 1, 0, 0.5f, 1f);
                        World.SpawnParticles(myParticles2);
                        ent.ReceiveDamage(new DamageSource()
                        {
                            Source = EnumDamageSource.Entity,
                            SourceEntity = this.FiredBy,
                            Type = EnumDamageType.Suffocation
                        }, 4f);
                    }

                }
            }
        }

    }

    public class Scullsparkle : Item
    {
        bool shoot = true;

        public override void OnHeldAttackStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, ref EnumHandHandling handling)
        {
            if (this.shoot == true)
            {
                float damage = 20;

                IPlayer byPlayer = null;
                if (byEntity is EntityPlayer) byPlayer = byEntity.World.PlayerByUid(((EntityPlayer)byEntity).PlayerUID);
                byEntity.World.PlaySoundAt(new AssetLocation("omniummod:sounds/item/scourgethrower"), byEntity, byPlayer, false, 8);

                EntityProperties type = byEntity.World.GetEntityType(new AssetLocation("omniummod:sparklescull"));// sparklescull
                Entity entity = byEntity.World.ClassRegistry.CreateEntity(type);
                ((Sparklescull)entity).FiredBy = byEntity;
                ((Sparklescull)entity).Damage = damage;


                Vec3d pos = byEntity.ServerPos.XYZ.Add(0, byEntity.LocalEyePos.Y, 0);
                Vec3d aheadPos = pos.AheadCopy(1.5f, byEntity.SidedPos.Pitch, byEntity.SidedPos.Yaw);
                Vec3d velocity = (aheadPos - pos) * 0.3;


                entity.ServerPos.SetPos(byEntity.SidedPos.AheadCopy(1.5f).XYZ.Add(0, byEntity.LocalEyePos.Y, 0));
                entity.ServerPos.Motion.Set(velocity * 0.2);



                entity.Pos.SetFrom(entity.ServerPos);
                entity.World = byEntity.World;
                ((Sparklescull)entity).SetRotation();

                byEntity.World.SpawnEntity(entity);

                //slot.Itemstack.Collectible.DamageItem(byEntity.World, byEntity, slot);


                this.shoot = false;
                byEntity.World.RegisterCallback((dt) =>
                {
                    this.shoot = true;
                }, 2500);

            }
            handling = EnumHandHandling.Handled;
        }
    }

    public class Sparklescull : EntityAgent
    {
        public Entity FiredBy;
        public float Damage;
        double my = 0;
        int livelength = 0;

        public override void Initialize(EntityProperties properties, ICoreAPI api, long InChunkIndex3d)
        {
            base.Initialize(properties, api, InChunkIndex3d);
            my = this.Pos.Motion.Y;
        }

        public virtual void SetRotation()
        {
            EntityPos pos = (World is IServerWorldAccessor) ? ServerPos : Pos;

            double speed = pos.Motion.Length();

            if (speed > 0.01)
            {
                pos.Pitch = 0;
                pos.Yaw =
                    (GameMath.PI + (float)Math.Atan2(pos.Motion.X / speed, pos.Motion.Z / speed))
                ;
                //pos.Roll =
                //    (-(float)Math.Asin(GameMath.Clamp(-pos.Motion.Y / speed, -1, 1)))
                //;
            }
        }

        public override void OnGameTick(float dt)
        {
            base.OnGameTick(dt);

            this.Ignite();
            this.ServerPos.Motion.Y = my;
            this.ServerPos.Roll = 0;
            this.ServerPos.Pitch = 0;

            livelength += 1;
            if (livelength >= 200) this.Die(EnumDespawnReason.Removed);

            SimpleParticleProperties myParticles = new SimpleParticleProperties(1, 1, ColorUtil.ColorFromRgba(255, 255, 0, 0), this.ServerPos.XYZ.AddCopy(-0.4, -0.4, -0.4), this.ServerPos.XYZ.AddCopy(0.4, 0.4, 0.4), new Vec3f(-0.1f, -0.1f, -0.1f), new Vec3f(0.1f, 0.1f, 0.1f), 1, 0, 0.5f, 1f);
            //myParticles.ParticleModel = EnumParticleModel.Quad;
            World.SpawnParticles(myParticles);
            Entity[] e = World.GetEntitiesAround(ServerPos.XYZ, 1f, 1f);
            bool gefunden = false;

            if (Alive == true)
            {
                foreach (Entity ent in e)
                {
                    if (!(ent is Sparklescull) && ent != FiredBy)
                    {
                        gefunden = true;
                    }

                }
                if (gefunden == true)
                {
                    Entity[] e2 = World.GetEntitiesAround(ServerPos.XYZ, 5f, 5f);
                    SimpleParticleProperties myParticles2 = new SimpleParticleProperties(300, 500, ColorUtil.ColorFromRgba(255, 255, 0, 0), this.ServerPos.XYZ.AddCopy(-2, -2, -2), this.ServerPos.XYZ.AddCopy(2, 2, 2), new Vec3f(-0.5f, -0.5f, -0.5f), new Vec3f(0.5f, 0.5f, 0.5f), 1, 0, 0.5f, 1f);
                    //myParticles2.ParticleModel = EnumParticleModel.Quad;
                    World.SpawnParticles(myParticles2);
                    foreach (Entity ent in e)
                    {
                        if (!(ent is Sparklescull) && ent != FiredBy)
                        {
                            ent.ReceiveDamage(new DamageSource()
                            {
                                Source = EnumDamageSource.Entity,
                                SourceEntity = this.FiredBy,
                                Type = EnumDamageType.Suffocation
                            }, Damage);
                            ent.Ignite();
                        }
                    }
                    this.Die(EnumDespawnReason.Removed);
                }
            }

        }

    }
}
