using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Commands.Commands.CommandPlayer
{
    public class SearchTreasureCommand : SubCommandContainer
    {
        public SearchTreasureCommand()
        {
            Aliases = new[] { "tesoro" };
            RequiredRole = RoleEnum.Player;
            Description = "Buscas un tesoro en el mapa actual.";
        }

        public override void Execute(TriggerBase trigger)
        {
            var gameTrigger = trigger as GameTrigger;

            if (gameTrigger.Character.Map.Id == gameTrigger.Character.Record.TreasureMapCoffre && gameTrigger.Character.Record.TreasureSearch != 0 && !gameTrigger.Character.isMultiLeadder)
            {
                var monsterGradeId = 1;
                while (monsterGradeId < 12)
                {
                    var level = gameTrigger.Character.Level;

                    if (level > 200)
                        level = 200;

                    if (level > monsterGradeId * 20 + 10)
                        monsterGradeId++;
                    else
                        break;
                }
                var MonsterId = 3724;

                var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(MonsterId, monsterGradeId);
                var position = new ObjectPosition(gameTrigger.Character.Map, gameTrigger.Character.Cell, (DirectionsEnum)5);
                var monster = new Monster(grade, new MonsterGroup(0, position));

                var fight = Singleton<FightManager>.Instance.CreatePvMFight(gameTrigger.Character.Map);
                fight.ChallengersTeam.AddFighter(gameTrigger.Character.CreateFighter(fight.ChallengersTeam));
                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));


                fight.StartPlacement();
                fight.ChallengersTeam.ToggleOption(FightOptionsEnum.FIGHT_OPTION_SET_CLOSED);
                fight.HideBlades();
                //fight.StartFighting();



                gameTrigger.Character.SendServerMessage("No puedes iniciar la pelea mientras usas el .maestro");



                ContextHandler.HandleGameFightJoinRequestMessage(gameTrigger.Character.Client,
                new GameFightJoinRequestMessage(gameTrigger.Character.Fighter.Id, (ushort)fight.Id));
                gameTrigger.Character.SaveLater();

                gameTrigger.Character.DisplayNotification("Acaba de aparecer un <b> cofre del tesoro </b>. Defiéndete !!", NotificationEnum.INFORMATION);
            }
            else if (gameTrigger.Character.Record.TreasureSearch != 0)
            {
                gameTrigger.Character.SendServerMessage("No puedes encontrar nada aquí. Seguir mirando. O quita el .maestro");

                #region Pistas Astrub
                if (gameTrigger.Character.Record.TreasureIndice == 1 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 0;
                    int posArriveY = -22;

                    gameTrigger.Character.SendServerMessage("[Pista]: Ofrezco mi consejo al mesero más valiente.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 2 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 7;
                    int posArriveY = -15;

                    gameTrigger.Character.SendServerMessage("[Pista]: *CRAAAK* dijo el cuervo desde lo alto de su templo en ruinas.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 3 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 4;
                    int posArriveY = -22;

                    gameTrigger.Character.SendServerMessage("[Pista]: Un pensamiento cruza por tu mente.\n", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.Say("/think Creo que creí haber visto un carro en medio del pasillo.");
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 4 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 3;
                    int posArriveY = -17;

                    gameTrigger.Character.SendServerMessage("[Pista] : ¿Crees que una caja fuerte puede llevar botas? Deberías ir y echarle un vistazo.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 5 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 4;
                    int posArriveY = -15;

                    gameTrigger.Character.SendServerMessage("[Pista] : Un transeúnte dice que vio este <b> Tesoro </b> en cuestión. Llevaba un arco y flechas.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 6 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 5;
                    int posArriveY = -21;

                    gameTrigger.Character.SendServerMessage("[Pista] : Estas dos estatuillas sentadas me ponen la piel de gallina.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 7 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 1;
                    int posArriveY = -19;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 8 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 4;
                    int posArriveY = -18;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 9 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 2;
                    int posArriveY = -20;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 10 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 1;
                    int posArriveY = -17;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 11 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 2;
                    int posArriveY = -16;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 12 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 7;
                    int posArriveY = -17;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 13 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 7;
                    int posArriveY = -21;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 14 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 5;
                    int posArriveY = -17;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 15 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 3;
                    int posArriveY = -18;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 16 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 4;
                    int posArriveY = -20;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 17 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 5;
                    int posArriveY = -15;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 18 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 3;
                    int posArriveY = -16;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 19 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {//Fix
                    int posArriveX = 6;
                    int posArriveY = -17;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 20 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = 4;
                    int posArriveY = -19;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                #endregion

                #region Pistas Bonta
                if (gameTrigger.Character.Record.TreasureIndice == 21 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -37;
                    int posArriveY = -57;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 22 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -35;
                    int posArriveY = -58;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 23 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = 33;
                    int posArriveY = -60;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 24 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -29;
                    int posArriveY = -60;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 25 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -27;
                    int posArriveY = -58;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 26 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -30;
                    int posArriveY = -57;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 27 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -33;
                    int posArriveY = -56;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 28 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -35;
                    int posArriveY = -54;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 29 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -32;
                    int posArriveY = -54;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 30 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -29;
                    int posArriveY = -54;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 31 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -27;
                    int posArriveY = -52;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 32 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -31;
                    int posArriveY = -52;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 33 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -35;
                    int posArriveY = -52;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 34 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -30;
                    int posArriveY = -52;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 35 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -26;
                    int posArriveY = -51;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 36 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -30;
                    int posArriveY = -50;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 37 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -32;
                    int posArriveY = -50;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 38 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -37;
                    int posArriveY = -54;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 39 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -29;
                    int posArriveY = -49;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 40 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -30;
                    int posArriveY = -61;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                #endregion

                #region Pistas Brâkmar
                if (gameTrigger.Character.Record.TreasureIndice == 41 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -28;
                    int posArriveY = 31;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 42 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -25;
                    int posArriveY = 31;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 43 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -23;
                    int posArriveY = 31;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 44 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -21;
                    int posArriveY = 32;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 45 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -23;
                    int posArriveY = 32;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 46 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -26;
                    int posArriveY = 32;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 47 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -28;
                    int posArriveY = 33;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 48 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -21;
                    int posArriveY = 33;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 49 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -22;
                    int posArriveY = 34;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 50 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -25;
                    int posArriveY = 34;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 51 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -28;
                    int posArriveY = 34;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 52 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -30;
                    int posArriveY = 35;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 53 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -31;
                    int posArriveY = 36;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 54 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -27;
                    int posArriveY = 36;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 55 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -24;
                    int posArriveY = 36;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 56 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -21;
                    int posArriveY = 37;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 57 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -23;
                    int posArriveY = 38;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 58 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -25;
                    int posArriveY = 40;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 59 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -20;
                    int posArriveY = 40;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                else if (gameTrigger.Character.Record.TreasureIndice == 60 && gameTrigger.Character.Record.TreasureMapCoffre != gameTrigger.Character.Map.Id)
                {
                    int posArriveX = -22;
                    int posArriveY = 41;

                    gameTrigger.Character.SendServerMessage("[Pista] : No se encontró pista para este <b> Tesoro </b>.", System.Drawing.Color.BurlyWood);
                    gameTrigger.Character.SendServerMessage("[GPS] : Estás en <b> [" + (gameTrigger.Character.Map.Position.X - posArriveX) + ", " + (posArriveY - gameTrigger.Character.Map.Position.Y) + "] </b> de la ubicación del tesoro.", System.Drawing.Color.BurlyWood);
                }
                #endregion

                if (!gameTrigger.Character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                {
                    gameTrigger.Character.AddEmote(EmotesEnum.COMPLETE_A_QUEST);
                }
                gameTrigger.Character.PlayEmote(EmotesEnum.COMPLETE_A_QUEST);
            }
            else
            {
                gameTrigger.Character.SendServerMessage("Debes iniciar una búsqueda del tesoro para usar este comando o quitar el .maestro.");
            }
        }
    }
}
