import { defineConfig } from "vitepress";
import { SearchPlugin } from "vitepress-plugin-search";

const version = "v0.1.0";

export default defineConfig({
  title: "大规模物流算法仿真验证平台",
  description: "项目文档",
  themeConfig: {
    externalLinkIcon: true,
    returnToTopLabel: true,
    siteTitle: "大规模物流算法仿真平台",
    nav: [
      { text: "首页", link: "/index" },
      { text: "开发文档", link: `/${version}/unity/index` },
      { text: "使用接口", link: `/${version}/api/index` },
    ],
    lastUpdated: true,
    outline: "deep",
    search: {
      provider: "local",
    },
    docFooter: {
      prev: "上一页",
      next: "下一页",
    },
    markdown: {
      container: {
        tipLabel: "提示",
        warningLabel: "警告",
        dangerLabel: "危险",
        infoLabel: "信息",
        detailsLabel: "详细信息",
      },
    },
    footer: {
      message: "Recent update: 2024-10-11",
      copyright: "Copyright © BUPT",
    },
    sidebar: {
      "/v0.1.0/intro/": [
        {
          text: "项目介绍",
          items: [{ text: "简介", link: `/${version}/intro/index` }],
        },
      ],
      "/v0.1.0/api/": [
        {
          text: "使用接口",
          items: [
            { text: "简介", link: `/${version}/api/index` },
            { text: "任务分配", link: `/${version}/api/assign` },
            { text: "运动状态", link: `/${version}/api/state` },
            { text: "控制命令", link: `/${version}/api/command` },
          ],
        },
      ],
      "/v0.1.0/unity/": [
        {
          text: "开发文档",
          items: [
            { text: "简介", link: `/${version}/unity/index` },
            {
              text: "Entity",
              items: [
                { text: "Config", link: `/${version}/unity/entity/config` },
                { text: "Stack", link: `/${version}/unity/entity/stack` },
                { text: "Lift", link: `/${version}/unity/entity/lift` },
                { text: "Obj", link: `/${version}/unity/entity/obj` },
                { text: "Task", link: `/${version}/unity/entity/task` },
              ],
            },
            {
              text: "UI",
              items: [
                { text: "Setting", link: `/${version}/unity/ui/setting` },
                { text: "Metric", link: `/${version}/unity/ui/metrics` },
                { text: "Toolbar", link: `/${version}/unity/ui/toolbar` },
              ],
            },
            {
              text: "任务分配",
              items: [
                { text: "Example", link: `/${version}/unity/task/example` },
              ],
            },
            {
              text: "路径算法",
              items: [
                { text: "Example", link: `/${version}/unity/algo/example` },
                { text: "A*", link: `/${version}/unity/algo/astar` },
              ],
            },
          ],
        },
      ],
    },
    vite: { plugins: [SearchPlugin] },
  },
});
