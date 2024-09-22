﻿using CommandSystem;
using HelpSense.Helper;
using PlayerRoles;
using PluginAPI.Core;
using System;
using System.Collections.Generic;

namespace HelpSense.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class BcCommand : ICommand
    {

        public string white = "white";

        public string Command { get; } = "BC";

        public string[] Aliases { get; } = new string[]
        {
            "广播"
        };

        public string Description { get; } = "全服聊天";

        //阿巴巴巴
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get((sender as CommandSender).SenderId);
            if (arguments.Count != 0 && !player.IsMuted && Plugin.Instance.Config.EnableChatSystem && player != null)
            {
                if (CollectionExtensions.At(arguments, 0).Contains("<"))
                {
                    response = "包含敏感字符";
                    return false;
                }

                string color = player.Team is Team.SCPs ? "red" : player.Team is Team.ChaosInsurgency ? "green" :
                        player.Team is Team.Scientists ? "yellow" : player.Team is Team.ClassD ? "orange" :
                        player.Team is Team.Dead ? "white" : player.Team is Team.FoundationForces ?
                        "#4EFAFF" : "white";
                XHelper.Allbroadcast($"<size={Plugin.Instance.Config.ChatSystemSize}>[<color={color}>{player.Team}</color>][全体]{player.Nickname}: {CollectionExtensions.At(arguments, 0)}</size>", 4, Broadcast.BroadcastFlags.Normal);
                Log.Info(player.Nickname + " 发送了 " + CollectionExtensions.At(arguments, 0));
                response = "发送成功";
                return true;
            }
            else
            {
                response = "发送失败，你被禁言或者信息为空或者聊天系统未启用";
                return false;
            }
        }
    }
}
