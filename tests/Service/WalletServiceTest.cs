﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Domain.Dtos.Wallet;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Service.Services;
using Xunit;
using Domain.Enums;

namespace Tests.Service
{
    public class WalletServiceTest : BaseServiceTest
    {
        private IWalletService _service;
        private Mock<IWalletRepository> _repository;

        
        public WalletServiceTest()
        {
            _repository = new Mock<IWalletRepository>();
            _service = new WalletService(_repository.Object, _mapper);
        }

        [Fact(DisplayName = "Create wallet type")]
        [Trait("Service", "Wallet")]
        public async void ShouldCreateWallet()
        {
            try
            {
                // Arrange
                var walletCreateDto = new WalletCreateDto()
                {
                    Name = _fakerName,
                    CloseDate = _fakerDate,
                    DueDate = _fakerDate,
                    Description = _fakerName,
                    WalletTypeId = Guid.NewGuid()
                };

                var wallet = _mapper.Map<Wallet>(walletCreateDto);
                wallet.UserId = _userAdminId;

                // Act
                _repository
                    .Setup(m => m.CreateAsync(wallet).Result)
                    .Returns(wallet);
                var result = await _service.CreateAsync(walletCreateDto, _userAdminId);
                var resultUserIdNull = await _service.CreateAsync(walletCreateDto, It.IsAny<Guid>());

                walletCreateDto.WalletTypeId = Guid.Empty;
                _repository
                    .Setup(m => m.CreateAsync(wallet).Result)
                    .Returns(wallet);

                var resultWalletTypeIdNull = await _service.CreateAsync(walletCreateDto, It.IsAny<Guid>());

                // Assert
                Assert.NotNull(result);
                Assert.Null(resultUserIdNull);
                Assert.Null(resultWalletTypeIdNull);
                Assert.Equal(walletCreateDto.Name, result.Name);
                Assert.Equal(walletCreateDto.CloseDate, result.CloseDate);
                Assert.Equal(walletCreateDto.DueDate, result.DueDate);
                Assert.Equal(walletCreateDto.Description, result.Description);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List wallet types")]
        [Trait("Service", "Wallet")]
        public async void ShouldListWallet()
        {
            try
            {
                // Arrange
                var listWallet = new List<Wallet>
                {
                    new Wallet() {
                        Id = Guid.NewGuid(),
                        Name = _fakerName,
                        CloseDate = _fakerDate,
                        DueDate = _fakerDate,
                        CurrentValue = 100,
                        Description = _fakerName,
                        CreatedAt = _fakerDate,
                        UpdatedAt = _fakerDate,
                        Entrances = new List<Entrance>(),
                        WalletType = new WalletType()
                    },
                    new Wallet() {
                        Id = Guid.NewGuid(),
                        Name = _fakerName,
                        CloseDate = _fakerDate,
                        DueDate = _fakerDate,
                        CurrentValue = 100,
                        Description = _fakerName,
                        CreatedAt = _fakerDate,
                        UpdatedAt = _fakerDate,
                        Entrances = new List<Entrance>(),
                        WalletType = new WalletType()
                    },
                    new Wallet() {
                        Id = Guid.NewGuid(),
                        Name = _fakerName,
                        CloseDate = _fakerDate,
                        DueDate = _fakerDate,
                        CurrentValue = 100,
                        Description = _fakerName,
                        CreatedAt = _fakerDate,
                        UpdatedAt = _fakerDate,
                        Entrances = new List<Entrance>(),
                        WalletType = new WalletType()
                    },
                };

                // Act
                _repository
                .Setup(m => m.FindAsyncWalletsUser(_userAdminId).Result)
                .Returns(listWallet);
                var result = await _service.FindAsyncWalletsUserIds(_userAdminId);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Count() == 3);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List wallet by id")]
        [Trait("Service", "Wallet")]
        public async void ShouldFindByIdAsync()
        {
            try
            {
                // Arrange
                var wallet = new Wallet()
                {
                    Id = Guid.NewGuid(),
                    Name = _fakerName,
                    CloseDate = _fakerDate,
                    DueDate = _fakerDate,
                    CurrentValue = 100,
                    Description = _fakerName,
                    CreatedAt = _fakerDate,
                    UpdatedAt = _fakerDate,
                    Entrances = new List<Entrance>(),
                    WalletType = new WalletType()
                };

                // Act
                _repository
                .Setup(m => m.FindByIdAsync(It.IsAny<Guid>()).Result)
                .Returns(wallet);
                var result = await _service.FindByIdAsync(It.IsAny<Guid>());

                // Assert
                Assert.NotNull(result);
                Assert.False(result.Id.Equals(Guid.Empty));
                Assert.Equal(wallet.Id, result.Id);
                Assert.Equal(wallet.CreatedAt, result.CreatedAt);
                Assert.Equal(wallet.UpdatedAt, result.UpdatedAt);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "List wallet by id and return update dto")]
        [Trait("Service", "Wallet")]
        public async void ShouldFindByIdUpdateAsync()
        {
            try
            {
                // Arrange
                var wallet = new Wallet()
                {
                    Id = Guid.NewGuid(),
                    Name = _fakerName,
                    CloseDate = _fakerDate,
                    DueDate = _fakerDate,
                    CurrentValue = 100,
                    Description = _fakerName,
                    CreatedAt = _fakerDate,
                    UpdatedAt = _fakerDate,
                    Entrances = new List<Entrance>(),
                    WalletType = new WalletType()
                };

                // Act
                _repository
                .Setup(m => m.FindByIdAsync(It.IsAny<Guid>()).Result)
                .Returns(wallet);
                var result = await _service.FindByIdUpdateAsync(It.IsAny<Guid>());

                // Assert
                Assert.NotNull(result);
                Assert.False(result.Id.Equals(Guid.Empty));
                Assert.Equal(wallet.Id, result.Id);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Return wallet values aggregated")]
        [Trait("Service", "Wallet")]
        public async void WalletsTotalValuesTest()
        {
            try
            {
                // Arrange
                var userId = Guid.NewGuid();
                var walletValuesDto = new List<WalletValuesDto>
                {
                    new WalletValuesDto{ TotalIncomes = 100, TotalExpanses = 50},
                    new WalletValuesDto{ TotalIncomes = 100, TotalExpanses = 50},
                    new WalletValuesDto{ TotalIncomes = 100, TotalExpanses = 50},
                    new WalletValuesDto{ TotalIncomes = 100, TotalExpanses = 50},
                    new WalletValuesDto{ TotalIncomes = 100, TotalExpanses = 50},
                };

                // Act
                _repository
                .Setup(m => m.FindAsyncWalletsValues(_userAdminId).Result)
                .Returns(walletValuesDto);

                var result = await _service.WalletsTotalValues(_userAdminId);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.WalletsValues.Count().Equals(walletValuesDto.Count()));
                Assert.True(result.TotalIncomes.Equals(500));
                Assert.True(result.TotalExpanses.Equals(250));
                _repository.Verify(r => r.FindAsyncWalletsValues(_userAdminId).Result, Times.Exactly(1));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Theory(DisplayName = "update wallet values")]
        [Trait("Service", "Wallet")]
        [InlineData(EntranceType.Income, 10, 210)]
        [InlineData(EntranceType.Expanse, 10, 190)]
        [InlineData(EntranceType.Income, 20, 220)]
        [InlineData(EntranceType.Income, 80, 280)]
        [InlineData(EntranceType.Expanse, 100, 100)]
        public async void ShouldUpdateWalletValues(EntranceType type, double value, double expectedValue)
        {
            try
            {
                // Arrange
                var id = Guid.NewGuid();
                var wallet = new Wallet()
                {
                    Id = id,
                    Name = _fakerName,
                    CloseDate = _fakerDate,
                    DueDate = _fakerDate,
                    CurrentValue = 200,
                    Description = _fakerName,
                    CreatedAt = _fakerDate,
                    UpdatedAt = _fakerDate,
                    Entrances = new List<Entrance>(),
                    WalletType = new WalletType()
                };


                // Act
                _repository
                .Setup(m => m.FindByIdAsync(It.IsAny<Guid>()).Result);

                var resultWalletNotFound = await _service.UpdateWalletValue(id, (int) type, value);

                _repository
                .Setup(m => m.FindByIdAsync(It.IsAny<Guid>()).Result)
                .Returns(wallet);

                _repository.Setup(r => r.SaveChangesAsync().Result).Returns(1);

                var result = await _service.UpdateWalletValue(id, (int)type, value);

                // Assert
                Assert.Null(resultWalletNotFound);
                Assert.NotNull(result);
                Assert.True(result.CurrentValue.Equals(expectedValue));
                _repository.Verify(r => r.FindByIdAsync(id).Result, Times.Exactly(2));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Update wallet type")]
        [Trait("Service", "Wallet")]
        public async void ShouldUpdateWallet()
        {
            try
            {
                // Arrange
                var walletId = Guid.NewGuid();

                var wallet = new Wallet()
                {
                    Id = walletId,
                    Name = _fakerName,
                    CreatedAt = _fakerDate,
                    UpdatedAt = _fakerDate
                };
                var walletUpdateDto = _mapper.Map<WalletUpdateDto>(wallet);

                // Act
                _repository
                .Setup(m => m.FindByIdAsync(walletId).Result)
                .Returns(wallet);

                _repository
                    .Setup(m => m.SaveChangesAsync().Result)
                    .Returns(1);

                var result = await _service.UpdateAsync(walletUpdateDto);

                // Assert
                Assert.NotNull(result);
                Assert.False(result.Id.Equals(Guid.Empty));
                Assert.Equal(_fakerName, result.Name);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Delete wallet type")]
        [Trait("Service", "Wallet")]
        public async void ShouldDeleteWallet()
        {
            try
            {
                // Arrange

                // Act
                _repository
                .Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
                var result = await _service.DeleteAsync(Guid.NewGuid());

                // Assert
                Assert.True(result);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Not Delete wallet type")]
        [Trait("Service", "Wallet")]
        public async void ShouldNotDeleteWallet()
        {
            try
            {
                // Arrange

                // Act
                _repository
                .Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);
                var result = await _service.DeleteAsync(Guid.NewGuid());

                // Assert
                Assert.False(result);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Assert.True(false);
            }
        }
    }
}