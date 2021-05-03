﻿using System.Collections.Generic;
using Domain.Dtos.Wallet;
using Domain.Dtos.WalletType;

namespace Web.ViewModels.Wallet
{
    public class WalletCreateViewModel
    {
        public WalletCreateDto Wallet { get; set; }
        public IEnumerable<WalletTypeResultDto> WalletTypes { get; set; }
    }
}