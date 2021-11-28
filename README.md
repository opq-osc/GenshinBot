# GenshinBot 原神QQ机器人

## 本项目正在活跃开发中，有任何建议或疑问，请提Issue

---

## 简介

一个基于[OPQ Bot](https://github.com/opq-osc/OPQ)的QQ机器人，利用米游社APP的API，提供原神游戏角色查询、每日任务及树脂提醒、个人深渊战绩查询、世界探索信息查询等功能。

## 优势

- **快速部署**：得益于OPQ的http和webSocket接口，仅需简单修改配置文件，即可在任何地方部署你的机器人
- **方便快捷**：本项目使用手机号与短信验证码登录米游社，无需提供Cookie，方便用户使用
- **加强安全**：短信验证码用后即销毁，明文传输无风险；精心设计的加密算法，用户敏感数据全部加密存储于数据库，无需担心Cookie泄露
- **依赖注入**：模块间松耦合，全部以服务形式注入.Net服务主机，可测试，可复用，易拓展
- **轻松拓展**：基于[YukinoshitaBot](https://github.com/opq-osc/YukinoshitaBot)框架搭建业务，你可以用类似MVC的架构轻松拓展自己的功能

## 如何使用

本项目正在开发中，尚未发布。

如果您是开发者，可自行克隆代码调试运行

如果您希望参与本项目的测试，可以[点击这里](https://qm.qq.com/cgi-bin/qm/qr?k=KeDGCjJwVtd02hwcE95yEsEVHFjNkQk7)加入我们的测试用QQ群

## 开发规划

| 功能 | 状态 |
| --- | --- |
| 短信验证码登录米游社 | 完成，可用 |
| 账号信息查询(文字) | 完成，可用 |
| 基本信息查询(文字) | 完成，可用 |
| 每日便签查询(文字) | 完成，可用 |
| 角色信息查询(文字) | API未完成 |
| 深渊战绩查询(文字) | API未完成 |
| 账号信息渲染(图片) | 未开始 |
| 基本信息渲染(图片) | 未开始 |
| 角色信息渲染(图片) | 未开始 |
| 每日便签渲染(图片) | 未开始 |
| 树脂及每日任务提醒 | 未开始 |

## 如何贡献

本项目正在活跃开发中，非常欢迎你的加入！[提一个 Issue](https://github.com/opq-osc/GenshinBot/issues/new) 或者提交一个 Pull Request。

## 维护者

[@opq-osc](https://github.com/opq-osc)
[@AZhrZho](https://github.com/AZhrZho)

## 致谢

[@Lightczx](https://gist.github.com/Lightczx) 提供了米游社客户端Salt逆向算法
